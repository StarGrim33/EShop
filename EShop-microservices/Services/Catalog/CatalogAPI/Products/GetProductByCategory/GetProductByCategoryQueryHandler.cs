namespace CatalogAPI.Products.GetProductByCategory
{
    public class
        GetProductByCategoryQueryHandler
        (IDocumentSession session, ILogger<GetProductByCategoryQueryHandler> logger) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductsByCategoryQueryHandler.Handle called with");
            var products = await session.Query<Product>()
                .Where(p => p.Category.Contains(query.Category))
                .ToListAsync(token: cancellationToken);
            return new GetProductByCategoryResult(products);
        }
    }

    public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;

    public record GetProductByCategoryResult(IEnumerable<Product> Products);
}
