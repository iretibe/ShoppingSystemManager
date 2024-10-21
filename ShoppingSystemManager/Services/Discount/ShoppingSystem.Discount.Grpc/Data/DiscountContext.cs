using Microsoft.EntityFrameworkCore;
using ShoppingSystem.Discount.Grpc.Models;

namespace ShoppingSystem.Discount.Grpc.Data
{
    public class DiscountContext : DbContext
    {
        public DiscountContext(DbContextOptions<DiscountContext> options) : base(options) { }

        public DbSet<Coupon> Coupons { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData
            (
                new Coupon { Id = 1, ProductName = "Rice K-75 50KG", Description = "This is a rice product", Amount = 950},
                new Coupon { Id = 2, ProductName = "Pasta Aroma Spaghetti", Description = "This is a spaghetti product", Amount = 300 }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
