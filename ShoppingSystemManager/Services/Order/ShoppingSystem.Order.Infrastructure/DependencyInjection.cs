using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ShoppingSystem.Order.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlDatabase");

            //services.AddDbContext<ApplicationDbContext>(options =>  options.UseSqlServer(connectionString));

            //services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            return services;
        }
    }
}
