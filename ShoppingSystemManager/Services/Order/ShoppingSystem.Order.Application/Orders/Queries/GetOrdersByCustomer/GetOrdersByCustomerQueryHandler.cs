using Microsoft.EntityFrameworkCore;
using ShoppingSystem.BuildingBlocks.CQRS;
using ShoppingSystem.Order.Application.Data;
using ShoppingSystem.Order.Application.Extensions;
using ShoppingSystem.Order.Core.ValueObjects;

namespace ShoppingSystem.Order.Application.Orders.Queries.GetOrdersByCustomer
{
    public class GetOrdersByCustomerQueryHandler(IShoppingContext shoppingContext) : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
    {
        public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery request, CancellationToken cancellationToken)
        {
            //Get orders by customer
            var orders = await shoppingContext.Orders
                .Include(o => o.OrderItems)
                .AsNoTracking()
                .Where(o => o.CustomerId == CustomerId.Of(request.CustomerId))
                .OrderBy(o => o.OrderName.Value)
                .ToListAsync();

            return new GetOrdersByCustomerResult(orders.ToOrderDtoList());
        }
    }
}
