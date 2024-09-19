using Carter;
using Mapster;
using MediatR;

namespace ShoppingSystem.Product.API.Products.UpdateProduct
{
    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateProductCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateProductResponse>();

                return Results.Ok(response);
            })
                .WithName("UpdateProduct")
                .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Update Product")
                .WithDescription("Update Product");
        }
    }

    public record UpdateProductRequest(Guid Id, string ProductCode, string Barcode, string ProductName, List<string> Category,
        string ProductDescription, string ProductImage, string Status, double InitialStock, double ReorderLevel,
        double StockLevel, decimal CostPrice, decimal SellingPrice, string CreateBy, Guid StoreId);

    public record UpdateProductResponse(bool IsSuccess);
}
