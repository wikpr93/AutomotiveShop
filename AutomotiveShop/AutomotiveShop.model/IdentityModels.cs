using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using AutomotiveShop.model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AutomotiveShop.model
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
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

    public class AutomotiveShopDbContext : IdentityDbContext<ApplicationUser>
    {
        public AutomotiveShopDbContext()
            : base("AutomotiveShopDbContext", throwIfV1Schema: false)
        {
        }

        public static AutomotiveShopDbContext Create()
        {
            return new AutomotiveShopDbContext();
        }

        public DbSet<CarDetails> CarsDetails { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Order> Orders { get; set; }
  
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductByCar> ProductsByCar  { get; set; }

        public DbSet<ProductCopy> ProductCopies { get; set; }

        public DbSet<ProductInOrder> ProductsInOrder { get; set; }

        public DbSet<Subcategory> Subcategories { get; set; }
    }
}