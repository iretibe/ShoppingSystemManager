using ShoppingSystem.Order.Core.Abstractions;

namespace ShoppingSystem.Order.Core.Events
{
    public record OrderUpdatedEvent(Models.Order order) : IDomainEvent;
}
