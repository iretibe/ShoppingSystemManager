using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using ShoppingSystem.BuildingBlocks.Messaging.Events;
using ShoppingSystem.Order.Application.Dtos;
using ShoppingSystem.Order.Application.Orders.Commands.CreateOrder;

namespace ShoppingSystem.Order.Application.Orders.EventHandlers.IntegrationEvents
{
    public class BasketCheckoutEventHandler(ISender sender, ILogger<BasketCheckoutEventHandler> logger) : IConsumer<BasketCheckoutEvent>
    {
        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            //Create a new order and start order fullfillment process
            logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);

            var command = MapToCreateOrderCommand(context.Message);

            await sender.Send(command).ConfigureAwait(false);
        }

        private CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent message)
        {
            //Create full order with incoming event data
            var addressDto = new AddressDto(
                message.FirstName, message.LastName, message.Email,
                message.AddressLine, message.Country, message.State, message.ZipCode);

            var paymentDto = new PaymentDto(
                message.CardName, message.CardNumber, message.Expiration, message.CVV, message.PaymentMethod);

            var orderId = Guid.NewGuid();

            var orderDto = new OrderDto(
                Id: orderId,
                CustomerId: message.CustomerId,
                OrderName: message.UserName,
                ShippingAddress: addressDto,
                BillingAddress: addressDto,
                Payment: paymentDto,
                Status: Core.Enums.OrderStatus.Pending,
                OrderItems:
                [
                    new OrderItemDto(orderId, new Guid("DCE11641-4CE0-4650-AFAA-EAE37FAC84A9"), 5, 455),
                    new OrderItemDto(orderId, new Guid("DD2127E7-D13F-4D80-BCF8-EEAA7C65A274"), 3, 470)
                ]);

            return new CreateOrderCommand(orderDto);
        }
    }
}
