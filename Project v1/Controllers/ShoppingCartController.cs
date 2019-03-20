using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project_v1.Entities;
using Project_v1.Models;
using Project_v1.Areas.Admin.Extensions;
using Project_v1.Extensions;

namespace Project_v1.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ShoppingCart
        public async Task<ActionResult> Index()
        {
            var item = await db.Database.SqlQuery<OrderDetail>("select * from orderdetail").ToListAsync();
            if (item == null)
                return HttpNotFound();
            var model = await item.Convert(db);
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> Index(int?itemId, int quantity)
        {
            var item = new Item();
            if ( itemId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var transaction =  db.Database.BeginTransaction();
            try
            {
                item = null;
                await db.Database.ExecuteSqlCommandAsync("update orderdetail set quantity = @p0 where itemid = @p1", quantity, itemId);
                item = await db.Database.SqlQuery<Item>("select * from item where id=@p0", itemId).SingleOrDefaultAsync();
                if (!item.Availability)
                {
                    transaction.Rollback();
                }
                else if (item.Availability && item.Quantity < quantity)
                {
                    transaction.Rollback();
                }else
                    transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
                return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<ActionResult> CheckOut()
        {
            List<OrderDetail> orderDetail  = null;
            try
            {
                orderDetail = await db.Database.SqlQuery<OrderDetail>("Select * from orderdetail").ToListAsync();
                var model = await orderDetail.Convert(db);

                var orderModel = await model.Convert(db, User.Identity.Name);
                if (orderDetail.Count().Equals(0))
                {
                    return RedirectToAction("Index", "item");
                }
                return View(orderModel);
            }
            catch
            {
                if (orderDetail == null)
                {
                    //later 
                }
                return RedirectToAction("Index", "item");
            }
        }

        public async Task<ActionResult> RemoveFromCart(int id)
        {
            try
            {
                var cartItem = await db.Database.SqlQuery<Item>("select * from item where id=@p0", id).SingleOrDefaultAsync();
                if (cartItem == null)
                    return HttpNotFound();
                if (Session["Cart"] != null)
                {
                    var Items = (List<Item>)Session["Cart"];
                    Items.Remove(cartItem);
                    Session["Cart"] = Items;
                }

                await db.Database.ExecuteSqlCommandAsync("delete from orderdetail where itemId = @p0", id);
                return RedirectToAction("Index");
            }
            catch
            {
                    return HttpNotFound();
            }
        }

        public async Task<ActionResult> ClearCart()
        {
            Session["cart"] = null;
            await db.Database.ExecuteSqlCommandAsync("delete from orderdetail");
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Order(OrderModel model,Payment payment)
        {
            bool fire = false;
            var transaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    if (model != null && payment != null)
                    {
                        var account = await db.Database.SqlQuery<Account>("select * from account where email=@p0", User.Identity.Name).SingleOrDefaultAsync();
                        model.FirstName = account.FirstName;
                        model.LastName = account.LastName;
                        model.Email = account.Email;
                        model.OrderDate = DateTime.Now;
                        await db.Database.ExecuteSqlCommandAsync("insert into [order] (firstname, lastname, address, city, country, province, postcode, phone, total, orderdate, email) values(@p0,@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10)",
                            model.FirstName, model.LastName, model.Address, model.City, model.Country, model.Province, model.PostCode, model.Phone, model.Total, model.OrderDate, model.Email
                            );
                    var found = await db.Database.SqlQuery<Order>("select * from [order] where email=@p0 and orderdate=@p1 and total = @p2", model.Email, model.OrderDate, model.Total).SingleOrDefaultAsync();
                //Deducting items quantity from stock
                var details = await db.Database.SqlQuery<OrderDetail>("select * from orderdetail").ToListAsync();
                //var details = await details.Convert(db);
                foreach (var de in details)
                {
                    var item = await db.Database.SqlQuery<Item>("select * from item where id = @p0", de.ItemId).SingleOrDefaultAsync();
                    var leftQuantity = item.Quantity - de.Quantity;
                    if (leftQuantity == 0)
                    {
                        await db.Database.ExecuteSqlCommandAsync("update item set quantity = @p0, availability = @p1 where id=@p2",leftQuantity,false,de.ItemId);
                    }
                    else if (leftQuantity >0)
                    {
                        await db.Database.ExecuteSqlCommandAsync("update item set quantity=@p0, availability=@p1 where id=@p2", leftQuantity, true, de.ItemId);
                        }
                        else
                        {
                            fire = true;
                            transaction.Rollback();
                            return RedirectToAction("response",new { msgType="error",msg="Sorry, Looks like we are out of items",title="Out Of Stock"}); //Quantity not available msg
                        }

                }
                    if (!fire)
                    {
                        await db.Database.ExecuteSqlCommandAsync("insert into payment (cardNumber,amount,paytype,email,paydate,orderid) values(@p0,@p1,@p2,@p3,@p4,@p5)",
        payment.CardNumber, model.Total, payment.PayType, model.Email, DateTime.Now, found.OrderId);
                        transaction.Commit();
                        return RedirectToAction("response", new { msgType = "success", msg = "Soon Your items will be at your home, Enjoy Shopping further", title = "Thank You For Your Order" }); ; // successfull view Thankyou page
                    }

                    }
                }
                catch
                {
                    transaction.Rollback();
                }
                
            return RedirectToAction("response", new { msgType = "error", msg = "Sorry, Some Problem occurr", title = "Technical Problem" }); // failure either something doesn't exist
        }

        public async Task<ActionResult> Response(string msgType, string msg, string title)
        {
            ViewBag.Type = msgType;
            ViewBag.Message = msg;
            ViewBag.Title = title;
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
