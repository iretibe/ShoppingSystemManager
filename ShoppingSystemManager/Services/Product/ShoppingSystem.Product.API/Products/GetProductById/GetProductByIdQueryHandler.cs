using Marten;
using MediatR;
using ShoppingSystem.BuildingBlocks.CQRS;
using ShoppingSystem.Product.API.Exceptions;

namespace ShoppingSystem.Product.API.Products.GetProductById
{
    internal class GetProductByIdQueryHandler(IDocumentSession session) : IRequestHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Models.Product>(request.Id, cancellationToken);

            if (product is null)
            {
                throw new ProductNotFoundException(request.Id);
            }

            return new GetProductByIdResult(product);
        }
    }

    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

    public record GetProductByIdResult(Models.Product product);
}
