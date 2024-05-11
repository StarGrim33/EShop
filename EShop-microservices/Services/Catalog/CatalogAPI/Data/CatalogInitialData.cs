using Marten.Schema;

namespace CatalogAPI.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        await using var session = store.LightweightSession();

        if (await session.Query<Product>().AnyAsync(cancellation))
            return;

        session.Store<Product>(GetPreconfiguredProducts());
        await session.SaveChangesAsync(cancellation);
    }

    private static List<Product> GetPreconfiguredProducts()
    {
        return
        [
            new Product
            {
                Id = new Guid(),
                Name = "IPhone X",
                Description = "Huge phone",
                ImageFile = "iphoneX.png",
                Price = 65000,
                Category = ["Smart Phone"]
            },
            new Product
            {
                Id = new Guid(),
                Name = "IPhone 11",
                Description = "Amazing phone",
                ImageFile = "iphone11.png",
                Price = 95000,
                Category = ["Smart Phone"]
            },
            new Product
            {
                Id = new Guid(),
                Name = "Samsung 10",
                Description = "Amazing phone",
                ImageFile = "samsung10.png",
                Price = 77000,
                Category = ["Smart Phone"]
            }
        ];
    }
}