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
    public class ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Product
        public async Task<ActionResult> Index()
        {
            var products = await db.Database.SqlQuery<Product>("select * from product").ToListAsync();
            var model = await products.Convert(db);
            return View(model);
        }

        // GET: Admin/Product/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Database.SqlQuery<Product>("select * from product where id=@p0", id).SingleOrDefaultAsync();
            if (product == null)
            {
                return HttpNotFound();
            }
            var model = await product.Convert(db);
            return View(model);
        }

        // GET: Admin/Product/Create
        public async Task<ActionResult> Create()
        {
            var model = new ProductModel
            {
                ProductTypes = await db.Database.SqlQuery<ProductType>("Select * from ProductType").ToListAsync()
            };
            return View(model);
        }

        // POST: Admin/Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Description,ImageUrl,ProductTypeId")] Product product)
        {
            if (ModelState.IsValid)
            {
                await db.Database.ExecuteSqlCommandAsync("Insert into product(name,ProductTypeId,ImageUrl,Description) values(@p0,@p1,@p2,@p3)", product.Name, product.ProductTypeId, product.ImageUrl,product.Description);

                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Admin/Product/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Database.SqlQuery<Product>("Select * from product where id=@p0", id).SingleOrDefaultAsync();
            if (product == null)
            {
                return HttpNotFound();
            }
            var prod = new List<Product>();
            prod.Add(product);
            var ProductModel = await prod.Convert(db);
            return View(ProductModel.First());
        }

        // POST: Admin/Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Description,ImageUrl,ProductTypeId")] Product product)
        {
            if (ModelState.IsValid)
            {
                await db.Database.ExecuteSqlCommandAsync("Update Product set name=@p0, ProductTypeId=@p1,imageUrl=@p2, Description=@p3 where id=@p4", product.Name, product.ProductTypeId, product.ImageUrl,product.Description, product.Id);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Admin/Product/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Database.SqlQuery<Product>("select * from product where id=@p0", id).SingleOrDefaultAsync();
            if (product == null)
            {
                return HttpNotFound();
            }
            var model = await product.Convert(db);
            return View(model);
        }

        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await db.Database.ExecuteSqlCommandAsync("Delete from product where id=@p0 ", id);
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
