using ShoppingSystem.BuildingBlocks.CQRS;
using ShoppingSystem.BuildingBlocks.Pagination;
using ShoppingSystem.Order.Application.Dtos;

namespace ShoppingSystem.Order.Application.Orders.Queries.GetOrders
{
    public record GetOrdersQuery(PaginationRequest PaginationRequest) : IQuery<GetOrdersResult>;
    
    public record GetOrdersResult(PaginationResult<OrderDto> Orders);
}
