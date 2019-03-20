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
    public class RecommendController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Recommend
        public async Task<ActionResult> Index()
        {
            //showing > 50% rated items
            var itemIds = await db.Database.SqlQuery<int>("select itemId from rateitem where rate >= 50").ToListAsync();
            var list = new List<Item>();
            foreach (var id in itemIds)
            {
                list.Add(await db.Database.SqlQuery<Item>("select * from item where id = @p0",id).SingleOrDefaultAsync());
            }
            //show sorry msg
            if (list.Count().Equals(0))
                return View();
            return View(await list.Convert(db));
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
