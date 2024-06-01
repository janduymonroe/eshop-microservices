
using Microsoft.AspNetCore.Mvc;

namespace EShopMicroservice.Basket.Api.Basket.DeleteBasket;

public record DeleteBasketRequest(string UserName);

public record DeleteBasketResponse(bool IsSuccess);

public class DeleteBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{userName}", 
                       async([AsParameters]DeleteBasketRequest request, ISender sender) =>
            {

                var command = request.Adapt<DeleteBasketCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<DeleteBasketResponse>();

                return Results.Ok(response);
            })
            .WithName("DeleteBasketById")
            .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Delete Basket By Id")
            .WithDescription("Delete Basket By Id");
    }
}
