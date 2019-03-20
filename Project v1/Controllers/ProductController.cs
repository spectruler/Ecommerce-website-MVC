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
    public class ProductController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Product
        public async Task<ActionResult> Index()
        {
            var products = await db.Database.SqlQuery<Product>("select * from product").ToListAsync();
            var model = await products.Convert(db);
            return View(model);
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
