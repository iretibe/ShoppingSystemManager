using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ShoppingSystem.Order.Infrastructure.Data.Extensions
{
    public static class DatabaseExtensions
    {
        public static async Task InitializeDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ShoppingContext>();

            context.Database.MigrateAsync().GetAwaiter().GetResult();

            await SeedAsync(context);
        }

        private static async Task SeedAsync(ShoppingContext context)
        {
            await SeedCustomerAsync(context);
            await SeedProductAsync(context);
            await SeedOrderItemsAsync(context);
        }

        private static async Task SeedOrderItemsAsync(ShoppingContext context)
        {
            //if (!await context.Orders.AnyAsync())
            //{
            //    await context.Orders.AddRangeAsync(InitialData.OrderWithItems);

            //    await context.SaveChangesAsync();
            //}

            if (!await context.Orders.AnyAsync())
            {
                var orders = InitialData.OrderWithItems.ToList();

                // Add each order individually
                foreach (var order in orders)
                {
                    context.Orders.Add(order);
                }

                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedProductAsync(ShoppingContext context)
        {
            if (!await context.Products.AnyAsync())
            {
                await context.Products.AddRangeAsync(InitialData.Products);

                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedCustomerAsync(ShoppingContext context)
        {
            if (!await context.Customers.AnyAsync())
            {
                await context.Customers.AddRangeAsync(InitialData.Customers);

                await context.SaveChangesAsync();
            }
        }
    }
}
