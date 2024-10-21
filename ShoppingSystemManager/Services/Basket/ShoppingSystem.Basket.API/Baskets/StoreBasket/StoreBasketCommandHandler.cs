using FluentValidation;
using ShoppingSystem.Basket.API.Models;
using ShoppingSystem.Basket.API.Repositories;
using ShoppingSystem.BuildingBlocks.CQRS;
using ShoppingSystem.Discount.Grpc;

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

    public class StoreBasketCommandHandler(IBasketRepository _repo, DiscountProtoService.DiscountProtoServiceClient _serviceClient) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
        {
            await DeductDiscount(request.Cart, cancellationToken);

            //ShoppingCart cart = request.Cart;

            await _repo.StoreBasketAsync(request.Cart, cancellationToken);

            return new StoreBasketResult(request.Cart.UserName);
        }

        private async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
        {
            //Communicate with discount GRPC and calculate latest prices of products into the basket
            foreach (var item in cart.Items)
            {
                var coupon = await _serviceClient.GetDiscountAsync(new GetDiscountRequest
                {
                    ProductName = item.ProductName
                });

                item.Price -= coupon.Price;
            }
        }
    }
}
