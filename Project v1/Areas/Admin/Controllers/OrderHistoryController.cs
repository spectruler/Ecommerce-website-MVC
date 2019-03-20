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

namespace Project_v1.Areas.Admin.Controllers
{
    [Authorize(Users ="admin@admin.com")]
    public class OrderHistoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/OrderHistory
        public async Task<ActionResult> Index()
        {
            var orderHistories = await db.Database.SqlQuery<OrderHistory>("select * from orderHistory").ToListAsync();
            var model = await orderHistories.Convert(db);
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
