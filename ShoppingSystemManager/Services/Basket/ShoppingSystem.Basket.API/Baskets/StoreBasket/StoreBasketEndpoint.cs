﻿using Carter;
using Mapster;
using MediatR;
using ShoppingSystem.Basket.API.Models;

namespace ShoppingSystem.Basket.API.Baskets.StoreBasket
{
    public record StoreBasketRequest(ShoppingCart ShoppingCart);
    public record StoreBasketResponse(string UserName);

    public class StoreBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket", async(StoreBasketRequest request, ISender sender) =>
            {
                var command = request.Adapt<StoreBasketCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<StoreBasketResponse>();
                
                return Results.Created($"/basket/{response.UserName}", response);
            })
                .WithName("CreateProduct")
                .Produces<StoreBasketResponse>(StatusCodes.Status201Created)
                .WithSummary("Create Product")
                .WithDescription("Create Product");
        }
    }
}
