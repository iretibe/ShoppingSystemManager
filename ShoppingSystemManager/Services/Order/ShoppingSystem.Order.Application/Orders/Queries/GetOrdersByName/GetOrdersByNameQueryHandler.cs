using Microsoft.EntityFrameworkCore;
using ShoppingSystem.BuildingBlocks.CQRS;
using ShoppingSystem.Order.Application.Data;
using ShoppingSystem.Order.Application.Dtos;
using ShoppingSystem.Order.Application.Extensions;

namespace ShoppingSystem.Order.Application.Orders.Queries.GetOrdersByName
{
    public class GetOrdersByNameQueryHandler(IShoppingContext shoppingContext) : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
    {
        public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery request, CancellationToken cancellationToken)
        {
            var orders = await shoppingContext.Orders
                .Include(o => o.OrderItems)
                .AsNoTracking()
                .Where(o => o.OrderName.Value.Contains(request.Name))
                .OrderBy(o => o.OrderName)
                .ToListAsync();
            
            return new GetOrdersByNameResult(orders.ToOrderDtoList());
        }
    }
}
