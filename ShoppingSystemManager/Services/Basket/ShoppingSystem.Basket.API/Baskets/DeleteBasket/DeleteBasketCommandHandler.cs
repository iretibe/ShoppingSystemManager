using FluentValidation;
using ShoppingSystem.Basket.API.Repositories;
using ShoppingSystem.BuildingBlocks.CQRS;

namespace ShoppingSystem.Basket.API.Baskets.DeleteBasket
{
    public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;
    public record DeleteBasketResult(bool IsSuccess);

    public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required!");
        }
    }

    public class DeleteBasketCommandHandler(IBasketRepository _repo) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            await _repo.DeleteBasketAsync(request.UserName, cancellationToken);

            return new DeleteBasketResult(true);
        }
    }
}
