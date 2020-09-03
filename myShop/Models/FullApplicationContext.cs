using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace myShop.Models
{
    public class FullApplicationContext : IdentityDbContext
    {
        public FullApplicationContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<ProductColour> ProductColours { get; set; }
        public DbSet<ShippingAddress> ShippingAddresses { get; set; }
        public DbSet<BillingAddress> BillingAddresses { get; set; }
    }
}
