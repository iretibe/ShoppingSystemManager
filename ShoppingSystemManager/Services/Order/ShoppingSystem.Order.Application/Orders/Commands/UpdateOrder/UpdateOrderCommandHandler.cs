using ShoppingSystem.BuildingBlocks.CQRS;
using ShoppingSystem.Order.Application.Data;
using ShoppingSystem.Order.Application.Dtos;
using ShoppingSystem.Order.Application.Exceptions;
using ShoppingSystem.Order.Core.ValueObjects;

namespace ShoppingSystem.Order.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler(IShoppingContext shoppingContext) : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
    {
        public async Task<UpdateOrderResult> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            //Update Order entity from command object
            var orderId = OrderId.Of(request.orderDto.Id);
            var order = await shoppingContext.Orders.FindAsync([orderId], cancellationToken);

            if (order == null) 
            {
                throw new OrderNotFoundException(request.orderDto.Id);
            }

            //Save to database
            UpdateOrderWithNewValues(order, request.orderDto);

            shoppingContext.Orders.Update(order);
            await shoppingContext.SaveChangesAsync(cancellationToken);

            //Return result
            return new UpdateOrderResult(true);
        }

        private void UpdateOrderWithNewValues(Core.Models.Order order, OrderDto orderDto)
        {
            var updatedShippingAddress = 
                Address.Of(orderDto.ShippingAddress.FirstName, 
                        orderDto.ShippingAddress.LastName, 
                        orderDto.ShippingAddress.Email, 
                        orderDto.ShippingAddress.AddressLine, 
                        orderDto.ShippingAddress.Country, 
                        orderDto.ShippingAddress.State, 
                        orderDto.ShippingAddress.ZipCode);

            var updatedBillingAddress = 
                Address.Of(orderDto.BillingAddress.FirstName, 
                            orderDto.BillingAddress.LastName, 
                            orderDto.BillingAddress.Email, 
                            orderDto.BillingAddress.AddressLine, 
                            orderDto.BillingAddress.Country, 
                            orderDto.BillingAddress.State, 
                            orderDto.BillingAddress.ZipCode);

            var updatedPayment = 
                Payment.Of(
                    orderDto.Payment.CardName, 
                    orderDto.Payment.CardNumber, 
                    orderDto.Payment.Expiration, 
                    orderDto.Payment.Cvv, 
                    orderDto.Payment.PaymentMethod);

            order.Update(
                orderName: OrderName.Of(orderDto.OrderName),
                shippingAddress: updatedShippingAddress,
                billingAddress: updatedBillingAddress,
                payment: updatedPayment,
                status: orderDto.Status);
        }
    }
}
