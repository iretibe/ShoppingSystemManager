﻿using Marten;
using ShoppingSystem.BuildingBlocks.CQRS;

namespace ShoppingSystem.Product.API.Products.GetProductByCategory
{
    internal class GetProductByCategoryQueryHandler(IDocumentSession session, ILogger<GetProductByCategoryQueryHandler> logger) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation(nameof(GetProductByCategoryQueryHandler));

            var entities = await session.Query<Models.Product>()
                .Where(p => p.Category.Contains(request.Category))
                .ToListAsync();

            return new GetProductByCategoryResult(entities);
        }
    }

    public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;

    public record GetProductByCategoryResult(IEnumerable<Models.Product> Products);
}