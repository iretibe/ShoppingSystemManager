using Microsoft.EntityFrameworkCore;
using ShoppingSystem.Order.Application.Data;
using ShoppingSystem.Order.Core.Models;
using System.Reflection;

namespace ShoppingSystem.Order.Infrastructure.Data
{
    public class ShoppingContext : DbContext, IShoppingContext
    {
        public ShoppingContext(DbContextOptions<ShoppingContext> options) : base(options)
        {
            
        }

        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Core.Models.Order> Orders => Set<Core.Models.Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
