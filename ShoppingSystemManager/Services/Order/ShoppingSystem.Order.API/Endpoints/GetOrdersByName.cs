using ShoppingSystem.Order.Application.Orders.Queries.GetOrdersByName;

namespace ShoppingSystem.Order.API.Endpoints
{
    //public record GetOrdersByNameRequest(string Name);
    public record GetOrdersByNameResponse(IEnumerable<OrderDto> Orders);


    public class GetOrdersByName : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            //Accept the ame parameter
            app.MapGet("/orders/{orderName}", async (string orderName, ISender sender) =>
            {
                var result = await sender.Send(new GetOrdersByNameQuery(orderName));

                //Return the result with the created orderId
                var response = result.Adapt<GetOrdersByNameResponse>();

                return Results.Ok(response);
            })
                .WithName("GetOrdersByName")
                .Produces<GetOrdersByNameResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Get Orders By Name")
                .WithDescription("Get Orders By Name");
        }
    }
}
