namespace CatalogAPI.Products.GetProductByCategory;

public class
    GetProductByCategoryQueryHandler(IDocumentSession session)
    : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query,
        CancellationToken cancellationToken)
    {
        var products = await session.Query<Product>()
            .Where(p => p.Category.Contains(query.Category))
            .ToListAsync(cancellationToken);
        return new GetProductByCategoryResult(products);
    }
}

public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;

public record GetProductByCategoryResult(IEnumerable<Product> Products);

public class GetProductByCategoryValidation : AbstractValidator<GetProductByCategoryQuery>
{
    public GetProductByCategoryValidation()
    {
        RuleFor(query => query.Category).NotEmpty().WithMessage("Category must be not empty");
    }
}