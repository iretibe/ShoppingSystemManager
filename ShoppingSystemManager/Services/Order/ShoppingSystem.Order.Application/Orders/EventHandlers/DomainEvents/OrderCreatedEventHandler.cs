using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using ShoppingSystem.Order.Application.Extensions;
using ShoppingSystem.Order.Core.Events;

namespace ShoppingSystem.Order.Application.Orders.EventHandlers.DomainEvents
{
    public class OrderCreatedEventHandler(IPublishEndpoint endpoint, IFeatureManager featureManager, ILogger<OrderCreatedEventHandler> _logger) : INotificationHandler<OrderCreatedEvent>
    {
        public async Task Handle(OrderCreatedEvent domainEventNotification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event handled: {DomainEvent}", domainEventNotification.GetType().Name);

            if (await featureManager.IsEnabledAsync("OrderFullfiment"))
            {
                var orderCreatedIntegrationEvent = domainEventNotification.order.ToOrderDto();
                await endpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
            }            
        }
    }
}
