using ShoppingSystem.Order.Application.Orders.Commands.CreateOrder;

namespace ShoppingSystem.Order.API.Endpoints
{
    public record CreateOrderRequest(OrderDto Order);
    public record CreateOrderResponse(Guid Id);

    public class CreateOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            //Accept a createOrderRequestObject
            app.MapPost("/orders", async (CreateOrderRequest request, ISender sender) =>
            {
                //Map the request to the CreateOrderCommand
                var command = request.Adapt<CreateOrderCommand>();

                //Use the MediatR to send the command to the corresponding handler
                var result = await sender.Send(command);

                //Return the result with the created orderId
                var response = result.Adapt<CreateOrderResponse>();

                return Results.Created($"/orders/{response.Id}", response);
            })
                .WithName("CreateOrder")
                .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create Order")
                .WithDescription("Create Order");
        }
    }
}
