using ShoppingSystem.BuildingBlocks.CQRS;
using ShoppingSystem.Order.Application.Dtos;

namespace ShoppingSystem.Order.Application.Orders.Queries.GetOrdersByCustomer
{
    public record GetOrdersByCustomerQuery(Guid CustomerId) : IQuery<GetOrdersByCustomerResult>;

    public record GetOrdersByCustomerResult(IEnumerable<OrderDto> OrderDtos);
}
