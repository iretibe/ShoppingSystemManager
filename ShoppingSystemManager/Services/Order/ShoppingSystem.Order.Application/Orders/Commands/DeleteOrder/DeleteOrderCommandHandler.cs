using ShoppingSystem.BuildingBlocks.CQRS;
using ShoppingSystem.Order.Application.Data;
using ShoppingSystem.Order.Application.Exceptions;
using ShoppingSystem.Order.Core.ValueObjects;

namespace ShoppingSystem.Order.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler(IShoppingContext shoppingContext) : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
    {
        public async Task<DeleteOrderResult> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            //Delete Order entity from command object
            var orderId = OrderId.Of(request.OrderId);
            var order = await shoppingContext.Orders.FindAsync(orderId, cancellationToken);

            //Save to database
            if (order == null) 
            {
                throw new OrderNotFoundException(request.OrderId);
            }

            shoppingContext.Orders.Remove(order);
            await shoppingContext.SaveChangesAsync(cancellationToken);

            //Return Result
            return new DeleteOrderResult(true);
        }
    }
}
