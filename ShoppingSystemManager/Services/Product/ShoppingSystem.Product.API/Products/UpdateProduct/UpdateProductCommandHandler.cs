using Marten;
using ShoppingSystem.BuildingBlocks.CQRS;
using ShoppingSystem.Product.API.Exceptions;

namespace ShoppingSystem.Product.API.Products.UpdateProduct
{
    internal class UpdateProductCommandHandler(IDocumentSession session, ILogger<UpdateProductCommandHandler> logger) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation(nameof(UpdateProductCommandHandler));

            var entity = await session.LoadAsync<Models.Product>(request.Id, cancellationToken);

            if (entity is null)
            {
                throw new ProductNotFoundException();
            }

            entity.Id = request.Id;
            entity.ProductName = request.ProductName;
            entity.Category = request.Category;
            entity.ProductDescription = request.ProductDescription;
            entity.ProductImage = request.ProductImage;
            entity.Status = request.Status;
            entity.InitialStock = request.InitialStock;
            entity.ReorderLevel = request.ReorderLevel;
            entity.StockLevel = request.StockLevel;
            entity.CostPrice = request.CostPrice;
            entity.SellingPrice = request.SellingPrice;
            entity.StoreId = request.StoreId;

            session.Update(entity);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(true);
        }
    }

    public record UpdateProductCommand(Guid Id, string ProductCode, string Barcode, string ProductName, List<string> Category,
        string ProductDescription, string ProductImage, string Status, double InitialStock, double ReorderLevel,
        double StockLevel, decimal CostPrice, decimal SellingPrice, string UpdateBy, Guid StoreId) : ICommand<UpdateProductResult>;

    public record UpdateProductResult(bool IsSuccess);
}
