using FluentValidation;
using ShoppingSystem.Basket.API.Models;
using ShoppingSystem.Basket.API.Repositories;
using ShoppingSystem.BuildingBlocks.CQRS;

namespace ShoppingSystem.Basket.API.Baskets.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(string UserName);

    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.Cart).NotNull().WithMessage("Cart can not be null");
            RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName is required!");
        }
    }

    public class StoreBasketCommandHandler(IBasketRepository _repo) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
        {
            ShoppingCart cart = request.Cart;

            await _repo.StoreBasketAsync(request.Cart, cancellationToken);

            return new StoreBasketResult(request.Cart.UserName);
        }
    }
}
