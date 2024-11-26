using MediatR;
using Microsoft.Extensions.Logging;
using ShoppingSystem.Order.Core.Events;

namespace ShoppingSystem.Order.Application.Orders.EventHandlers
{
    public class OrderUpdatedEventHandler(ILogger<OrderUpdatedEventHandler> _logger) : INotificationHandler<OrderUpdatedEvent>
    {
        public Task Handle(OrderUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Domain Event handled: {DomainEvent}", notification.GetType().Name);

            return Task.CompletedTask;
        }
    }
}
