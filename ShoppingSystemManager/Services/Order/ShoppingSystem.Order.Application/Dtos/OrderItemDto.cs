namespace ShoppingSystem.Order.Application.Dtos
{
    public record OrderItemDto(
        Guid OrderId,
        Guid ProductId,
        int QUantity,
        decimal Price);
}
