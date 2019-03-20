using Project_v1.Entities;
using Project_v1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace Project_v1.Extensions
{
    public static class ItemOrderConvertExtension
    {
        public static async Task<IEnumerable<ItemOrderModel>> Convert(this IEnumerable<Item> items,ApplicationDbContext db, bool NotItemModel)
        {
            var itemOrders = new List<ItemOrderModel>();
            if (items.Count().Equals(0))
                return itemOrders;
            return from io in items
                   select new ItemOrderModel
                   {
                       ItemId = io.Id,
                       ItemName = io.Name,
                       ProductId = io.ProductId,
                       AvailableQuantity = io.Quantity,
                       Availablity = io.Availability,
                       ImageUrl = io.ImageUrl,
                       UnitPrice = io.Price,
                       ProductName = getProductName(io.Id, db),
                       ItemQuantity = getItemQuantity(io.Id, db),
                       ItemDescription = io.Description
                   };
            
        }

        public static async Task<IEnumerable<ItemOrderModel>> Convert(this IEnumerable<OrderDetail> orderDetails, ApplicationDbContext db)
        {
            var itemOrders = new List<ItemOrderModel>();
            if (orderDetails.Count().Equals(0))
                return itemOrders;
            return from od in orderDetails
                   select new ItemOrderModel
                   {
                       ItemId = (int)od.ItemId,
                       ItemName = getItemName(od.ItemId,db),
                       ProductId = getItemProductId(od.ItemId,db),
                       AvailableQuantity = getItemQuantity(od.ItemId,db),
                       Availablity = getItemAvailability(od.ItemId,db),
                       ImageUrl = getItemImageUrl(od.ItemId,db),
                       UnitPrice = (double)od.UnitPrice,
                       ProductName = getProductName(getItemProductId(od.ItemId,db), db),
                       ItemQuantity = od.Quantity,
                       ItemDescription = getItemDescription(od.ItemId,db)
                   };

        }


        public static async Task<OrderModel> Convert(this IEnumerable<ItemOrderModel> orderDetails,ApplicationDbContext db,string email)
        {
            
            var user = await db.Database.SqlQuery<Account>("select * from account where email = @p0", email).SingleOrDefaultAsync();
            if (orderDetails.Count().Equals(0))
            {
                return new OrderModel();
            }
            var orders =  new OrderModel{ Email=user.Email,FirstName=user.FirstName,LastName=user.LastName,Total=getTotalAmount(db)};
            orders.OrderDetails = orderDetails;
            return orders;
        }


        private static  string getProductName(int? id,ApplicationDbContext db)
        {
            if (id == null)
                return null;
            var product =   db.Database.SqlQuery<Product>("Select * from product where id=@p0",id).SingleOrDefault();
            return product.Name;
        }

        private static int getItemQuantity(int? id,ApplicationDbContext db)
    {
            if (id == null)
                return 1;
        var quantity =  db.Database.SqlQuery<Item>("Select * from item where id=@p0", id).SingleOrDefault();
        return quantity.Quantity;
    }

        public static string getItemName(int? id, ApplicationDbContext db)
        {
            if (id == null)
                return null;
            var product = db.Database.SqlQuery<Item>("Select * from item where id=@p0", id).SingleOrDefault();
            return product.Name;
        }

        private static string getItemImageUrl(int? id, ApplicationDbContext db)
        {
            if (id == null)
                return null;
            var product = db.Database.SqlQuery<Item>("Select * from item where id=@p0", id).SingleOrDefault();
            return product.ImageUrl;
        }
        private static string getItemDescription(int? id, ApplicationDbContext db)
        {
            if (id == null)
                return null;
            var product = db.Database.SqlQuery<Item>("Select * from item where id=@p0", id).SingleOrDefault();
            return product.Description;
        }

        private static bool getItemAvailability(int? id, ApplicationDbContext db)
        {
            var product = db.Database.SqlQuery<Item>("Select * from item where id=@p0", id).SingleOrDefault();
            return product.Availability;
        }

        private static int? getItemProductId(int? id, ApplicationDbContext db)
        {
            var product = db.Database.SqlQuery<Item>("Select * from item where id=@p0", id).SingleOrDefault();
            return product.ProductId;
        }

        private static decimal getTotalAmount(ApplicationDbContext db)
        {
            decimal total = 0;
            var list = db.Database.SqlQuery<OrderDetail>("Select * from orderdetail").ToList();
            foreach (var od in list)
            {
                total += od.Quantity * od.UnitPrice;
            }
            return total;
        }
    }
}