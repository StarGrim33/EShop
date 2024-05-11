﻿namespace CatalogAPI.Products.GetProducts;

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ([AsParameters] GetProductsRequest request, ISender sender) =>
            {
                var query = request.Adapt<GetProductsQuery>();
                var result = await sender.Send(query);
                var response = result.Adapt<GetProductsResponse>();
                return Results.Ok(response);
            }).WithName("GetProduct")
            .Produces<GetProductsResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product")
            .WithDescription("Get Product");
    }
}

public record GetProductsResponse(IEnumerable<Product> Products);

public record GetProductsRequest(int? PageNumber = 1, int? PageSize = 10);