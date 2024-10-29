using ArabityAuth.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ArabityAuth.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Cart>()
                .HasKey(b => new {b.CustomerId,b.ProductParcode});

            builder.Entity<Favourite>()
                .HasKey(b => new { b.CustomerId, b.ProductParcode });

            builder.Entity<OrderProduct>()
                .HasKey(b => new { b.OrderNumbre, b.ProductParcode });

            builder.Entity<OrderStore>()
                .HasKey(b => new { b.OrderNumbre, b.StoreId });
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Store> Stores { get; set; }

        public DbSet<Workshop> Workshops { get; set; }

        public DbSet<StoreProduct> StoreProducts { get; set; }

        public DbSet<ProductComment> productComments { get; set; }

        public DbSet<StoreComment> StoreComments { get; set; }

        public DbSet<WorkshopComment> workshopComments { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<Favourite> Favorites { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderProduct> OrderProducts { get; set; }

        public DbSet<OrderStore> OrderStores { get; set; }

        public DbSet<WorkshopCarType> WorkshopCarTypes { get; set; }

    }
}