using Microsoft.EntityFrameworkCore;

namespace ShoppingSystem.Discount.Grpc.Data
{
    public static class Extensions
    {
        public static IApplicationBuilder UseMigration(this IApplicationBuilder builder)
        {
            using var scope = builder.ApplicationServices.CreateScope();
            
            using var dbContext = scope.ServiceProvider.GetRequiredService<DiscountContext>();
            dbContext.Database.MigrateAsync();

            return builder;
        }
    }
}
