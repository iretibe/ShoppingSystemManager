using Marten;
using Marten.Pagination;
using ShoppingSystem.BuildingBlocks.CQRS;

namespace ShoppingSystem.Product.API.Products.GetAllProducts
{
    public record GetAllProductsQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetAllProductsResult>;

    public record GetAllProductsResult(IEnumerable<Models.Product> Products);

    internal class GetAllProductsQueryHandler(IDocumentSession session) : IQueryHandler<GetAllProductsQuery, GetAllProductsResult>
    {
        public async Task<GetAllProductsResult> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            //var entities = await session.Query<Models.Product>().ToListAsync(cancellationToken);
            var entities = await session.Query<Models.Product>().ToPagedListAsync(request.PageNumber ?? 1, request.PageSize ?? 10, cancellationToken);

            return new GetAllProductsResult(entities);
        }
    }
}
