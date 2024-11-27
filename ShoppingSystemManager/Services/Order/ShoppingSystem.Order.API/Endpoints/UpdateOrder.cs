using ShoppingSystem.Order.Application.Orders.Commands.UpdateOrder;

namespace ShoppingSystem.Order.API.Endpoints
{
    public record UpdateOrderRequest(OrderDto Order);
    public record UpdateOrderResponse(bool IsSuccess);


    public class UpdateOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            //Accept a UpdateOrderRequest Object
            app.MapPut("/orders", async (UpdateOrderRequest request, ISender sender) =>
            {
                //Map the request to the UpdateOrderCommand
                var command = request.Adapt<UpdateOrderCommand>();

                //Send the command for processing
                var result = await sender.Send(command);

                //Return the result with the created orderId
                var response = result.Adapt<UpdateOrderResponse>();

                return Results.Ok(response);
            })
                .WithName("UpdateOrder")
                .Produces<UpdateOrderResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Update Order")
                .WithDescription("Update Order");
        }
    }
}
