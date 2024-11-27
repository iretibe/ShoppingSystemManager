using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using ShoppingSystem.BuildingBlocks.Behaviors;
using ShoppingSystem.BuildingBlocks.Messaging.MassTransit;
using System.Reflection;

namespace ShoppingSystem.Order.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
                cfg .AddOpenBehavior(typeof(LoggingBehavior<,>));
            });

            services.AddFeatureManagement();
            services.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
