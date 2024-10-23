using ShoppingSystem.Order.Core.Exceptions;

namespace ShoppingSystem.Order.Core.ValueObjects
{
    public record OrderItemId
    {
        public Guid Value { get; }

        private OrderItemId(Guid value) => Value = value;

        public static OrderItemId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                throw new OrderDomainException("OrderItemId cannot be empty!");
            }

            return new OrderItemId(value);
        }
    }
}
