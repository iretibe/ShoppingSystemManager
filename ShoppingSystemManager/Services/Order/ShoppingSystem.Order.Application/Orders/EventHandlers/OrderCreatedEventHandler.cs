using MediatR;
using Microsoft.Extensions.Logging;
using ShoppingSystem.Order.Core.Events;

namespace ShoppingSystem.Order.Application.Orders.EventHandlers
{
    public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> _logger) : INotificationHandler<OrderCreatedEvent>
    {
        public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event handled: {DomainEvent}", notification.GetType().Name);
            
            return Task.CompletedTask;
        }
    }
}
