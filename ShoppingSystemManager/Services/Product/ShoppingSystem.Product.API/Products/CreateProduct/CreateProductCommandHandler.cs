using Marten;
using MediatR;
using ShoppingSystem.BuildingBlocks.CQRS;

namespace ShoppingSystem.Product.API.Products.CreateProduct
{
    //Create Command
    public record CreateProductCommand(string ProductCode, string Barcode, string ProductName, List<string> Category, 
        string ProductDescription, string ProductImage, string Status, double InitialStock, double ReorderLevel,
        double StockLevel, decimal CostPrice, decimal SellingPrice, string CreateBy, Guid StoreId) : ICommand<CreateProductResult>;
    
    public record CreateProductResult(Guid Id);

    internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var entity = new Models.Product
            {
                ProductCode = command.ProductCode,
                Barcode = command.Barcode,
                ProductName = command.ProductName,
                Category = command.Category,
                ProductDescription = command.ProductDescription,
                ProductImage = command.ProductImage,
                Status = command.Status,
                InitialStock = command.InitialStock,
                ReorderLevel = command.ReorderLevel,
                StockLevel = command.StockLevel,
                CostPrice = command.CostPrice,
                SellingPrice = command.SellingPrice,
                CreateBy = command.CreateBy,
                CreateDate = DateTime.Now,
                StoreId = command.StoreId
            };

            //Save to database
            session.Store(entity);
            await session.SaveChangesAsync(cancellationToken);

            return new CreateProductResult(entity.Id);
        }
    }
}
