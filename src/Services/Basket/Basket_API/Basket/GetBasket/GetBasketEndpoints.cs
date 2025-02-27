﻿using Carter;
using Mapster;
using MediatR;

namespace Basket_API.Basket.GetBasket;


//public record GetBasketRequest(string UserName) : IQuery<GetBasketResponse>;

public record GetBasketResponse(ShoppingCart Cart);

public class GetBasketEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
        {
            var command = new GetBasketRequest(userName);

            var result = await sender.Send(command);

            var response = result.Adapt<GetBasketResponse>();

            return Results.Ok(response);
        })
         .WithName("GetBasket")
         .Produces<GetBasketResponse>(StatusCodes.Status200OK)
         .ProducesProblem(StatusCodes.Status400BadRequest)
         .WithSummary("Get Basket")
         .WithDescription("Get Basket");
    }
}
