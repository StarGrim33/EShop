namespace CatalogAPI.Products.GetProductById
{
    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id:guid}", async (Guid id, ISender sender) =>
            {
                var result = sender.Send(new GetProductByIdQuery(id));
                var response = result.Adapt<GetProductByIdResponse>();
                return Results.Ok(response);
            }).WithName("GetProductById")
            .Produces<GetProductByIdResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product By Id")
            .WithDescription("Get Product By Id");
        }
    }

    public record GetProductByIdResponse(Product Product);
}
