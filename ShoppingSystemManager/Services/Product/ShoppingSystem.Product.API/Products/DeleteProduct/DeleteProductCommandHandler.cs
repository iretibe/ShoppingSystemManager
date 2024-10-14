using FluentValidation;
using Marten;
using ShoppingSystem.BuildingBlocks.CQRS;

namespace ShoppingSystem.Product.API.Products.DeleteProduct
{
    internal class DeleteProductCommandHandler(IDocumentSession session, ILogger<DeleteProductCommandHandler> logger) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation(nameof(DeleteProductCommandHandler));

            session.Delete<Models.Product>(request.Id);
            await session.SaveChangesAsync(cancellationToken);

            return new DeleteProductResult(true);
        }
    }

    public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;

    public record DeleteProductResult(bool IsSuccess);

    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty().WithMessage("Product Id is required!");
        }
    }
}
