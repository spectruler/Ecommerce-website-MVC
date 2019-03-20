using Project_v1.Areas.Admin.Models;
using Project_v1.Entities;
using Project_v1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Project_v1.Extensions;

namespace Project_v1.Areas.Admin.Extensions
{
    public static class ConversionExtensions
    {
        public static async Task<IEnumerable<ProductTypeModel>> Convert(this IEnumerable<ProductType> productTypes, ApplicationDbContext db)
        {
            if (productTypes.Count().Equals(0))
                return new List<ProductTypeModel>();
            var names = await db.Database.SqlQuery<Category>("select * from Category").ToListAsync();

            return from pt in productTypes
                   select new ProductTypeModel
                   {
                       Id = pt.Id,
                       Name = pt.Name,
                       CategoryId = pt.CategoryId,
                       ImageUrl = pt.ImageUrl,
                       Categories = names
                   };
        }

        public static async Task<ProductTypeModel> Convert(this ProductType productTypes, ApplicationDbContext db)
        {
            var names = await db.Database.SqlQuery<Category>("select * from Category where id=@p0 ",productTypes.CategoryId).FirstOrDefaultAsync(p => p.Id.Equals(productTypes.CategoryId));

            var model = new ProductTypeModel
            {
                Id = productTypes.Id,
                Name = productTypes.Name,
                CategoryId = productTypes.CategoryId,
                ImageUrl = productTypes.ImageUrl,
                Categories = new List<Category>()
                   };
            model.Categories.Add(names);
            return model;
        }

        public static async Task<IEnumerable<ProductModel>> Convert(this IEnumerable<Product> product, ApplicationDbContext db)
        {
            if (product.Count().Equals(0))
                return new List<ProductModel>();
            var names = await db.Database.SqlQuery<ProductType>("select * from ProductType").ToListAsync();

            return from pt in product
                   select new ProductModel
                   {
                       Id = pt.Id,
                       Name = pt.Name,
                       Description = pt.Description,
                       ProductTypeId = pt.ProductTypeId,
                       ImageUrl = pt.ImageUrl,
                       ProductTypes = names
                   };
        }

        public static async Task<ProductModel> Convert(this Product product, ApplicationDbContext db)
        {
            var names = await db.Database.SqlQuery<ProductType>("select * from ProductType where id=@p0 ", product.ProductTypeId).FirstOrDefaultAsync(p => p.Id.Equals(product.ProductTypeId));

            var model = new ProductModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ProductTypeId = product.ProductTypeId,
                ImageUrl = product.ImageUrl,
                ProductTypes = new List<ProductType>()
            };
            model.ProductTypes.Add(names);
            return model;
        }

        public static async Task<IEnumerable<ItemModel>> Convert(this IEnumerable<Item> item, ApplicationDbContext db)
        {
            if (item.Count().Equals(0))
                return new List<ItemModel>();
            var names = await db.Database.SqlQuery<Product>("select * from Product").ToListAsync();

            return from it in item
                   select new ItemModel
                   {
                       Id = it.Id,
                       Name = it.Name,
                       Description = it.Description,
                       ProductId = it.ProductId,
                       ImageUrl = it.ImageUrl,
                       Price = it.Price,
                       Availability = it.Availability,
                       Quantity = it.Quantity,
                       Products = names
                   };
        }

        public static async Task<ItemModel> Convert(this Item item, ApplicationDbContext db)
        {
            var names = await db.Database.SqlQuery<Product>("select * from Product where id=@p0 ", item.ProductId).FirstOrDefaultAsync(p => p.Id.Equals(item.ProductId));

            var model = new ItemModel
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                ProductId = item.ProductId,
                ImageUrl = item.ImageUrl,
                Price = item.Price,
                Availability = item.Availability,
                Quantity = item.Quantity,
                Products = new List<Product>()
            };
            model.Products.Add(names);
            return model;
        }

        public static async Task<IEnumerable<OrderHistoryModel>> Convert(this IEnumerable<OrderHistory> productTypes, ApplicationDbContext db)
        {
            if (productTypes.Count().Equals(0))
                return new List<OrderHistoryModel>();

            return from pt in productTypes
                   select new OrderHistoryModel
                   {
                       OrderId = pt.OrderId,
                       PayId = pt.PayId,
                       ItemId = pt.ItemId,
                       Email = pt.Email,
                       UnitPrice = pt.UnitPrice,
                       ItemQuantity = pt.ItemQuantity,
                       ItemName = ItemOrderConvertExtension.getItemName(pt.ItemId,db),
                       date = getOrderDate(pt.OrderId, db),
                   };
        }

        private static DateTime getOrderDate(int? orderId, ApplicationDbContext db)
        {
            return db.Database.SqlQuery<DateTime>("select orderdate from [order] where orderid = @p0", orderId).SingleOrDefault();
        }

    }
}