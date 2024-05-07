namespace CatalogAPI.Products.UpdateProduct;

internal sealed class UpdateProductCommandHandler(IDocumentSession session, ILogger<UpdateProductCommandHandler> logger)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateProductHandler.Handle called with {@Command}", command);
        var product = await session.LoadAsync<Product>(command.Id, cancellationToken) ??
                      throw new ProductNotFoundException();
        product.Name = command.Name;
        product.Description = command.Description;
        product.Category = command.Category;
        product.Price = command.Price;
        product.ImageFile = command.ImageFile;

        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(true);
    }
}

public record UpdateProductCommand(
    Guid Id,
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price) : ICommand<UpdateProductResult>;

public record UpdateProductResult(bool IsSuccess);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(command => command.Id).NotEmpty().WithMessage("Product ID is required");
        RuleFor(command => command.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .Length(2, 150).WithMessage("Name must be between 2 and 150 characters");
        RuleFor(command => command.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than zero");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
        RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
    }
}