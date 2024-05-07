namespace CatalogAPI.Products.GetProductById
{
    internal sealed class GetProductByIdQueryHandler
        (IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger)
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductsByIdQueryHandler.Handle called with");
            var product = await session.LoadAsync<Product>(query.Id, cancellationToken) ?? throw new ProductNotFoundException();
            return new GetProductByIdResult(product);
        }
    }

    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

    public record GetProductByIdResult(Product Product);
}
