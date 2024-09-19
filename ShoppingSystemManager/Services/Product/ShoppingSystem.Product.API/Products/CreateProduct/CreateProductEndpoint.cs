using Carter;
using Mapster;
using MediatR;

namespace ShoppingSystem.Product.API.Products.CreateProduct
{
    public record CreateProductRequest(string ProductCode, string Barcode, string ProductName, List<string> Category,
        string ProductDescription, string ProductImage, string Status, double InitialStock, double ReorderLevel,
        double StockLevel, decimal CostPrice, decimal SellingPrice, string CreateBy, Guid StoreId);

    public record CreateProductResponse(Guid Id);

    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateProductCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateProductResponse>();

                return Results.Created($"/products/{response.Id}", response);
            })
                .WithName("CreateProduct")
                .Produces<CreateProductResponse>(StatusCodes.Status201Created)
                .WithSummary("Create Product")
                .WithDescription("Create Product");
        }
    }
}
