using ShoppingSystem.BuildingBlocks.CQRS;
using ShoppingSystem.Order.Application.Dtos;

namespace ShoppingSystem.Order.Application.Orders.Queries.GetOrdersByName
{
    public record GetOrdersByNameQuery(string Name) : IQuery<GetOrdersByNameResult>;

    public record GetOrdersByNameResult(IEnumerable<OrderDto> OrderDtos);
}
