using Carter;
using Mapster;
using MediatR;

namespace ShoppingSystem.Product.API.Products.GetProductById
{
    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id}", async (Guid id, ISender sender, ILogger<GetProductByIdEndpoint> logger) =>
            {
                logger.LogInformation("Received request to get product by ID: {Id}", id);

                var result = await sender.Send(new GetProductByIdQuery(id));

                logger.LogInformation("Product data before mapping: {@result}", result);

                var response = result.Adapt<GetProductByIdResponse>();

                logger.LogInformation("Mapped response: {@response}", response);

                return Results.Ok(response);
            })
                .WithName("GetByProductId")
                .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Get By Product Id")
                .WithDescription("Get By Product Id");
        }
    }

    public record GetProductByIdResponse(Models.Product Product);
}
