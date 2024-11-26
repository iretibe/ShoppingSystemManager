using ShoppingSystem.BuildingBlocks.CQRS;
using ShoppingSystem.Order.Application.Data;
using ShoppingSystem.Order.Application.Dtos;
using ShoppingSystem.Order.Core.ValueObjects;

namespace ShoppingSystem.Order.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler(IShoppingContext shoppingContext) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
    {
        public async Task<CreateOrderResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            //Create Order entity from command object
            var order = CreateNewOrder(request.Order);

            //Save to database
            shoppingContext.Orders.Add(order);
            await shoppingContext.SaveChangesAsync(cancellationToken);

            //return result
            return new CreateOrderResult(order.Id.Value);
        }

        private Core.Models.Order CreateNewOrder(OrderDto order)
        {
            var shippingAddress = Address.Of(order.ShippingAddress.FirstName, order.ShippingAddress.LastName, order.ShippingAddress.Email, order.ShippingAddress.AddressLine, order.ShippingAddress.Country, order.ShippingAddress.State, order.ShippingAddress.ZipCode);
            var billingAddress = Address.Of(order.BillingAddress.FirstName, order.BillingAddress.LastName, order.BillingAddress.Email, order.BillingAddress.AddressLine, order.BillingAddress.Country, order.BillingAddress.State, order.BillingAddress.ZipCode);

            var newOrder = Core.Models.Order.Create(
                id: OrderId.Of(Guid.NewGuid()),
                customerId: CustomerId.Of(order.CustomerId),
                orderName: OrderName.Of(order.OrderName),
                shippingAddress: shippingAddress,
                billingAddress: billingAddress,
                payment: Payment.Of(order.Payment.CardName, order.Payment.CardNumber, order.Payment.Expiration, order.Payment.Cvv, order.Payment.PaymentMethod)
                );

            foreach (var orderItem in order.OrderItems)
            {
                newOrder.Add(ProductId.Of(orderItem.ProductId), orderItem.QUantity, orderItem.Price);
            }

            return newOrder;
        }
    }
}
