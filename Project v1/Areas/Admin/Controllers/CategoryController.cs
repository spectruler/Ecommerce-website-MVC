using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Project_v1.Entities;
using Project_v1.Models;
using System.Data;
using System.Data.Entity;

namespace Project_v1.Areas.Admin.Controllers
{
    [Authorize(Users = "admin@admin.com")]
    public class CategoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Category
        public async Task<ActionResult> Index()
        {
            var CategoryList = await db.Database.SqlQuery<Category>("select * from category").ToListAsync();
            return View(CategoryList);
        }

        // GET: Admin/Category/Details/5
        public async Task<ActionResult> Details(int? id) //debug needed from id
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = await db.Database.SqlQuery<Category>("select * from category where id=@p0", id).SingleOrDefaultAsync();
            if (category == null)
            {
                return HttpNotFound();
            }

                return View(category);
        }

        // GET: Admin/Category/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Admin/Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,ImageUrl")] Category category)
        {
            if (ModelState.IsValid)
            {
                int output = await db.Database.ExecuteSqlCommandAsync("Insert into category(Name,ImageUrl) Values(@p0,@p1)", category.Name,category.ImageUrl);
                if (output > 0)
                {
                    ViewBag.msg = "Category Added"; //successfull
                }
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Admin/Category/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = await db.Database.SqlQuery<Category>("Select * from category where id = @p0", id).SingleOrDefaultAsync();
            if (category == null)
            {
                return HttpNotFound();
            }
                return View(category);
        }

        // POST: Admin/Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,ImageUrl")]Category category)
        {
            if (ModelState.IsValid)
            {
                await db.Database.ExecuteSqlCommandAsync("Update Category set Name=@p0, ImageUrl=@p1 where id=@p2 ", category.Name,category.ImageUrl,category.Id);
                return RedirectToAction("Index");
            }
                return View(category);

        }

        // GET: Admin/Category/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = await db.Database.SqlQuery<Category>("select * from category where id=@p0", id).SingleOrDefaultAsync();
            if (category == null)
            {
                return HttpNotFound();
            }
                return View(category);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            await db.Database.ExecuteSqlCommandAsync("Delete from category where id=@p0", id);
            return RedirectToAction("Index");
        }
    }
}
