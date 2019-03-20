using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Project_v1.Entities;

namespace Project_v1.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("desktop-qm8t64g", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        DbSet<Account> Accounts { get; set; }
        DbSet<Category>Categories { get; set; }
        DbSet<ProductType> ProductTypes { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Item> Items { get; set; }
        //DbSet<ShoppingCart> ShoppingCarts { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<OrderDetail> OrderDetails { get; set; }
        DbSet<Payment> Payments { get; set; }
        DbSet<OrderHistory> OrderHistories { get; set; }
        DbSet<PurchaseItemHistory> PurchaseItemHistories { get; set; }
        DbSet<RateItem> RateItems { get; set; }
    }
}