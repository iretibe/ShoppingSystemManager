using FluentValidation;
using Mapster;
using MassTransit;
using ShoppingSystem.Basket.API.Dtos;
using ShoppingSystem.Basket.API.Repositories;
using ShoppingSystem.BuildingBlocks.CQRS;
using ShoppingSystem.BuildingBlocks.Messaging.Events;

namespace ShoppingSystem.Basket.API.Baskets.CheckoutBasket
{
    public record CheckoutBasketCommand(BasketCheckoutDto BasketCheckoutDto) : ICommand<CheckoutBasketResult>;
    public record CheckoutBasketResult(bool IsSuccess);

    public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
    {
        public CheckoutBasketCommandValidator()
        {
            RuleFor(x => x.BasketCheckoutDto).NotNull().WithMessage("BasketCheckoutDto cannot be null!");
            RuleFor(x => x.BasketCheckoutDto.UserName).NotEmpty().WithMessage("UserName should not be empty!");
        }
    }


    public class CheckoutBasketCommandHandler(IBasketRepository repository, IPublishEndpoint endpoint) : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
    {
        public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand request, CancellationToken cancellationToken)
        {
            //Get existing basket records by user name
            var basket = await repository.GetBasketAsync(request.BasketCheckoutDto.UserName, cancellationToken);

            if (basket == null) 
            {
                return new CheckoutBasketResult(false);
            }

            //Set total price on basket event message
            var eventMessage = request.BasketCheckoutDto.Adapt<BasketCheckoutEvent>();
            eventMessage.TotalPrice = basket.TotalPrice;

            //Send basket checkout event to RabbitMQ using MassTransit
            await endpoint.Publish(eventMessage, cancellationToken);

            //Delete the basket
            await repository.DeleteBasketAsync(request.BasketCheckoutDto.UserName, cancellationToken).ConfigureAwait(false);

            return new CheckoutBasketResult(true);
        }
    }
}
