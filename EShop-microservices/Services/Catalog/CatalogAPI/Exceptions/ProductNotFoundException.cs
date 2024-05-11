using Building_Blocks.Exceptions;

namespace CatalogAPI.Exceptions
{
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(Guid id) : base("Product", id)
        {
        }
    }
}
