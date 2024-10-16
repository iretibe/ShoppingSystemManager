using FluentValidation;
using Marten;
using ShoppingSystem.BuildingBlocks.CQRS;
using ShoppingSystem.Product.API.Exceptions;

namespace ShoppingSystem.Product.API.Products.UpdateProduct
{
    internal class UpdateProductCommandHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await session.LoadAsync<Models.Product>(request.Id, cancellationToken);

            if (entity is null)
            {
                throw new ProductNotFoundException(request.Id);
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

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty().WithMessage("Product Id is required!");

            RuleFor(command => command.ProductName).NotEmpty()
                .WithMessage("Product name is required!")
                .Length(2, 150).WithMessage("Product Name must be between 2 and 150 characters!");

            RuleFor(command => command.SellingPrice).GreaterThan(0).WithMessage("Selling Price must be greater than 0");
        }
    }
}
