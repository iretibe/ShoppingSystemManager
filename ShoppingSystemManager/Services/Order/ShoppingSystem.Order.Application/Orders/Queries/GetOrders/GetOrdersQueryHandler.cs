using Microsoft.EntityFrameworkCore;
using ShoppingSystem.BuildingBlocks.CQRS;
using ShoppingSystem.BuildingBlocks.Pagination;
using ShoppingSystem.Order.Application.Data;
using ShoppingSystem.Order.Application.Extensions;

namespace ShoppingSystem.Order.Application.Orders.Queries.GetOrders
{
    public class GetOrdersQueryHandler(IShoppingContext shoppingContext) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
    {
        public async Task<GetOrdersResult> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            //Get orders with pagination
            var pageIndex = request.PaginationRequest.PageIndex;
            var pageSize = request.PaginationRequest.PageSize;

            var totalCount = await shoppingContext.Orders.LongCountAsync(cancellationToken);

            var orders = await shoppingContext.Orders
                .Include(o => o.OrderItems)
                .OrderBy(o => o.OrderName.Value)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            //Return result
            return new GetOrdersResult(new PaginationResult<Dtos.OrderDto>(pageIndex, pageSize, totalCount, orders.ToOrderDtoList()));
        }
    }
}
