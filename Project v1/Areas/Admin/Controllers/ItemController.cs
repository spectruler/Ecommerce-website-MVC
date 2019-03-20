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
using Project_v1.Areas.Admin.Models;

namespace Project_v1.Areas.Admin.Controllers
{
    [Authorize(Users ="admin@admin.com")]
    public class ItemController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Item
        public async Task<ActionResult> Index()
        {
            var items = await db.Database.SqlQuery<Item>("select * from Item").ToListAsync();
            var model = await items.Convert(db);
            return View(model);
        }

        // GET: Admin/Item/Details/5
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

        // GET: Admin/Item/Create
        public async Task<ActionResult> Create()
        {
            var model = new ItemModel
            {
                Products = await db.Database.SqlQuery<Product>("Select * from Product").ToListAsync()
            };
            return View(model);
        }

        // POST: Admin/Item/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Description,ImageUrl,ProductId,Price,Availability,Quantity")] Item item)
        {
            if (ModelState.IsValid)
            {
                await db.Database.ExecuteSqlCommandAsync("Insert into Item(name,Description,ImageUrl,ProductId,Price,Availability,Quantity) values(@p0,@p1,@p2,@p3,@p4,@p5,@p6)",
                    item.Name,item.Description,item.ImageUrl,item.ProductId,item.Price,item.Quantity == 0?false:true,item.Quantity);

                return RedirectToAction("Index");
            }
            return View(item);
        }

        // GET: Admin/Item/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = await db.Database.SqlQuery<Item>("Select * from item where id=@p0", id).SingleOrDefaultAsync();
            if (item == null)
            {
                return HttpNotFound();
            }
            var itm = new List<Item>();
           itm.Add(item);
            var ItemModel = await itm.Convert(db);
            return View(ItemModel.First());
        }

        // POST: Admin/Item/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Description,ImageUrl,ProductId,Price,Availability,Quantity")] Item item)
        {
            if (ModelState.IsValid)
            {
                await db.Database.ExecuteSqlCommandAsync("update item set name=@p0,description=@p1,ImageUrl=@p2,ProductId=@p3,Price=@p4,Availability=@p5,Quantity=@p6 where id=@p7",
                    item.Name,item.Description,item.ImageUrl,item.ProductId,item.Price,item.Quantity==0?false:true,item.Quantity,item.Id);
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // GET: Admin/Item/Delete/5
        public async Task<ActionResult> Delete(int? id)
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

        // POST: Admin/Item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await db.Database.ExecuteSqlCommandAsync("Delete from item where id=@p0 ", id);
            return RedirectToAction("Index");
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
