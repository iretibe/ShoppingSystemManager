using ShoppingSystem.Order.Core.Abstractions;

namespace ShoppingSystem.Order.Core.Events
{
    public record OrderCreatedEvent(Models.Order order) : IDomainEvent;
}
