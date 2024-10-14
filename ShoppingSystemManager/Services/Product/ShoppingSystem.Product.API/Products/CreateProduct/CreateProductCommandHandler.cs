using FluentValidation;
using Marten;
using ShoppingSystem.BuildingBlocks.CQRS;

namespace ShoppingSystem.Product.API.Products.CreateProduct
{
    //Create Command
    public record CreateProductCommand(string ProductCode, string Barcode, string ProductName, List<string> Category, 
        string ProductDescription, string ProductImage, string Status, double InitialStock, double ReorderLevel,
        double StockLevel, decimal CostPrice, decimal SellingPrice, string CreateBy, Guid StoreId) : ICommand<CreateProductResult>;
    
    public record CreateProductResult(Guid Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.ProductName).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(x => x.ProductImage).NotEmpty().WithMessage("ProductImage is required");
            RuleFor(x => x.SellingPrice).GreaterThan(0).WithMessage("Selling Price must be greater than 0");
        }
    }

    //internal class CreateProductCommandHandler(IDocumentSession session, IValidator<CreateProductCommand> validator) 
    //    : ICommandHandler<CreateProductCommand, CreateProductResult>
    internal class CreateProductCommandHandler(IDocumentSession session, ILogger<CreateProductCommandHandler> logger) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            //var result = await validator.ValidateAsync(command, cancellationToken);
            //var errors = result.Errors.Select(x => x.ErrorMessage).ToList();
            //if (errors.Any()) 
            //{
            //    throw new ValidationException(errors.FirstOrDefault());
            //}

            logger.LogInformation("CreateProductCommandHandler.Handle called with @command");

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
