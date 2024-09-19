using Carter;
using Mapster;
using MediatR;

namespace ShoppingSystem.Product.API.Products.GetAllProducts
{
    public class GetAllProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async (ISender sender) =>
            {
                var result = await sender.Send(new GetAllProductsQuery());

                var response = result.Adapt<GetAllProductsResponse>();

                return Results.Ok(response);
            })
                .WithName("GetAllProducts")
                .Produces<GetAllProductsResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get All Products")
                .WithDescription("Get All Products");
        }
    }

    public record GetAllProductsResponse(IEnumerable<Models.Product> Products);
}
