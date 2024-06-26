﻿namespace CatalogAPI.Products.GetProductById
{
    internal sealed class GetProductByIdQueryHandler
        (IDocumentSession session)
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(query.Id, cancellationToken) ?? throw new ProductNotFoundException(query.Id);
            return new GetProductByIdResult(product);
        }
    }

    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

    public record GetProductByIdResult(Product Product);

    public class GetProductByIdValidation : AbstractValidator<GetProductByIdQuery>
    {
        public GetProductByIdValidation()
        {
            RuleFor(query => query.Id).NotEmpty().WithMessage("Id must be greater than 0 and not empty");
        }
    }
}
