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
    [Authorize(Users = "admin@admin.com")]
    public class ProductTypeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/ProductType
        public async Task<ActionResult> Index()
        {
            var productTypes = await db.Database.SqlQuery<ProductType>("select * from producttype").ToListAsync();
            //var productTypes = db.ProductTypes.Include(p => p.Category);
            var model = await productTypes.Convert(db);
            return View(model);
        }

        // GET: Admin/ProductType/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductType productType = await db.Database.SqlQuery<ProductType>("select * from producttype where id=@p0", id).SingleOrDefaultAsync();
            if (productType == null)
            {
                return HttpNotFound();
            }
            var model = await productType.Convert(db);
            return View(model);
        }

        // GET: Admin/ProductType/Create
        public async Task<ActionResult> Create()
        {
            var model = new ProductTypeModel
            {
                Categories = await db.Database.SqlQuery<Category>("Select * from category").ToListAsync()
            };
            return View(model);
        }

        // POST: Admin/ProductType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,CategoryId,ImageUrl")] ProductType productType)
        {
            if (ModelState.IsValid)
            {
                await db.Database.ExecuteSqlCommandAsync("Insert into productType(name,categoryId,ImageUrl) values(@p0,@p1,@p2)", productType.Name, productType.CategoryId, productType.ImageUrl);
                 
                return RedirectToAction("Index");
            }
            return View(productType);
        }
        
        // GET: Admin/ProductType/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductType productType = await db.Database.SqlQuery<ProductType>("Select * from productType where id=@p0", id).SingleOrDefaultAsync();
            if (productType == null)
            {
                return HttpNotFound();
            }
            var prod = new List<ProductType>();
            prod.Add(productType);
            var ProductTypeModel = await prod.Convert(db);
            return View(ProductTypeModel.First());
        }
        
        // POST: Admin/ProductType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,CategoryId,ImageUrl")] ProductType productType)
        {
            if (ModelState.IsValid)
            {
                await db.Database.ExecuteSqlCommandAsync("Update ProductType set name=@p0, categoryId=@p1,imageUrl=@p2 where id=@p3", productType.Name, productType.CategoryId, productType.ImageUrl,productType.Id);
                return RedirectToAction("Index");
            }
            return View(productType);
        }
        // GET: Admin/ProductType/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductType productType = await db.Database.SqlQuery<ProductType>("select * from producttype where id=@p0", id).SingleOrDefaultAsync();
            if (productType == null)
            {
                return HttpNotFound();
            }
            var model = await productType.Convert(db);
            return View(model);
        }

        // POST: Admin/ProductType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await db.Database.ExecuteSqlCommandAsync("Delete from productType where id=@p0 ",id);
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
