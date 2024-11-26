using ShoppingSystem.BuildingBlocks.Exceptions;

namespace ShoppingSystem.Order.Application.Exceptions
{
    public class OrderNotFoundException : NotFoundException
    {
        public OrderNotFoundException(Guid id) : base("Order", id)
        {
        }
    }
}
