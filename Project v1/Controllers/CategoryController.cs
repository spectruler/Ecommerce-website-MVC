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

namespace Project_v1.Controllers
{
    public class CategoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Category
        public async Task<ActionResult> Index()
        {
            var CategoryList = await db.Database.SqlQuery<Category>("select * from category").ToListAsync();
            return View(CategoryList);
        }

        // GET: Category/Details/5
        public async Task<ActionResult> Details(int? id)
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
