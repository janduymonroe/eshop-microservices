using BuldingBlocks.Exceptions;

namespace EShopMicroservice.Catalog.Api.Exceptions;

public class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(Guid id) : base("Product", id)
    {
        
    }
}
