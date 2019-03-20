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

namespace Project_v1.Controllers
{
    public class ItemController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Item
        public async Task<ActionResult> Index()
        {
            var items = await db.Database.SqlQuery<Item>("select * from Item").ToListAsync();
            var model = await items.Convert(db);
            return View(model);
        }

        // GET: Item/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = await db.Database.SqlQuery<Item>("select * from item where id=@p0", id).SingleOrDefaultAsync();
            if (item == null)
            {
                return HttpNotFound();
            }
            var model = await item.Convert(db);
            return View(model);
        }

        
        public async Task<ActionResult> AddToCart(int? id)
        {
            var cartItem = new Item();
            var transaction = db.Database.BeginTransaction();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            cartItem = await db.Database.SqlQuery<Item>("select * from item where id=@p0", id).SingleOrDefaultAsync(); 
                try
            {
                var details = await db.Database.SqlQuery<OrderDetail>("Select * from orderdetail where itemid = @p0", cartItem.Id).ToListAsync();
                if (details.Count().Equals(1))
                {

                    //do nothing
                }
                else
                {
                    if (Session["Cart"] == null)
                    {
                        var item = new List<Item>();
                        item.Add(cartItem);
                        Session["Cart"] = item;
                    }
                    else
                    {
                        var Items = (List<Item>)Session["Cart"];
                        Items.Add(cartItem);
                        Session["Cart"] = Items;
                    }
                    if (cartItem == null)
                        return HttpNotFound();
                    await db.Database.ExecuteSqlCommandAsync("Insert into OrderDetail(ItemId,Quantity,unitPrice) values(@p0,@p1,@p2)", cartItem.Id, 1, cartItem.Price);

                }

            }
            catch
            {
                if (Session["Cart"] == null)
                {
                    var item = new List<Item>();
                    item.Add(cartItem);
                    Session["Cart"] = item;
                }
                else
                {
                    var Items = (List<Item>)Session["Cart"];
                    Items.Add(cartItem);
                    Session["Cart"] = Items;
                }
                if (cartItem == null)
                    return HttpNotFound();
                await db.Database.ExecuteSqlCommandAsync("Insert into OrderDetail(ItemId,Quantity,unitPrice) values(@p0,@p1,@p2)", cartItem.Id, 1, cartItem.Price);
            }
            if (!cartItem.Availability || cartItem.Quantity <= 0)
            {
                transaction.Rollback();
                var Items = (List<Item>)Session["Cart"];
                Items.Remove(cartItem);
                Session["Cart"] = Items;
            }
            else
            {
                transaction.Commit();
            }

                //await db.Database<ShoppingCart>.ExecuteSqlCommandAsync("Insert into ShoppingCart(emailAddress,count,ItemId,) Values(@p0,@p1,@p2)",Session["UserEmail"],)
                return RedirectToAction("Index","Item");
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
