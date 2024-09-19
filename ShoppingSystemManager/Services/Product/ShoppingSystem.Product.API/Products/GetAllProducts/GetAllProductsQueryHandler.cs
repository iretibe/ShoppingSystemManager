using Marten;
using ShoppingSystem.BuildingBlocks.CQRS;

namespace ShoppingSystem.Product.API.Products.GetAllProducts
{
    public record GetAllProductsQuery() : IQuery<GetAllProductsResult>;

    public record GetAllProductsResult(IEnumerable<Models.Product> Products);

    internal class GetAllProductsQueryHandler(IDocumentSession session, ILogger<GetAllProductsQueryHandler> logger) : IQueryHandler<GetAllProductsQuery, GetAllProductsResult>
    {
        public async Task<GetAllProductsResult> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"{nameof(GetAllProductsQueryHandler)}");

            var entities = await session.Query<Models.Product>().ToListAsync(cancellationToken);

            return new GetAllProductsResult(entities);
        }
    }
}
