using BuldingBlocks.Exceptions;

namespace EShopMicroservice.Ordering.Application.Exceptions;

public class OrderNotFoundException : NotFoundException
{
    public OrderNotFoundException(Guid id) : base("Order", id)
    {
    }
}
