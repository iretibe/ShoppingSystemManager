using Microsoft.EntityFrameworkCore;
using ShoppingSystem.Order.Core.Models;

namespace ShoppingSystem.Order.Application.Data
{
    public interface IShoppingContext
    {
        DbSet<Customer> Customers { get; }
        DbSet<Product> Products { get; }
        DbSet<Core.Models.Order> Orders { get; }
        DbSet<OrderItem> OrderItems { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
