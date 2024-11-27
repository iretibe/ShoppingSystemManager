using ShoppingSystem.Order.Application.Orders.Commands.DeleteOrder;

namespace ShoppingSystem.Order.API.Endpoints
{
    //public record DeleteOrderRequest(Guid Id);
    public record DeleteOrderResponse(bool IsSuccess);


    public class DeleteOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            //Accept The OrderId as parameter
            app.MapDelete("/orders/{id}", async (Guid Id, ISender sender) =>
            {
                //Construct the DeleteCommandOrder
                var result = await sender.Send(new DeleteOrderCommand(Id));

                //Return the result with the created orderId
                var response = result.Adapt<DeleteOrderResponse>();

                return Results.Ok(response);
            })
                .WithName("DeleteOrder")
                .Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Delete Order")
                .WithDescription("Delete Order");
        }
    }
}
