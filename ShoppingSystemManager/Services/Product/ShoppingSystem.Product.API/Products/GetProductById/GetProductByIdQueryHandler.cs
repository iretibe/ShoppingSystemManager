using Marten;
using MediatR;
using ShoppingSystem.BuildingBlocks.CQRS;
using ShoppingSystem.Product.API.Exceptions;

namespace ShoppingSystem.Product.API.Products.GetProductById
{
    internal class GetProductByIdQueryHandler(IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger) : IRequestHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Handling {nameof(GetProductByIdQueryHandler)} for Product Id: {request.Id}");

            var product = await session.LoadAsync<Models.Product>(request.Id, cancellationToken);

            if (product is null)
            {
                logger.LogError("Product with ID {ProductId} not found", request.Id);
                throw new ProductNotFoundException();
            }

            logger.LogInformation("Product with ID {ProductId} found: {@product}", request.Id, product);

            return new GetProductByIdResult(product);
        }
    }

    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

    public record GetProductByIdResult(Models.Product product);
}
