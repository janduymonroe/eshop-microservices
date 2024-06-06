namespace EShopMicroservice.Ordering.Domain.ValueObject;

public record OrderId : IEntityId
{
    public Guid Value { get; }
    private OrderId(Guid value) => Value = value;

    public static OrderId Of(Guid id)
    {
        ArgumentNullException.ThrowIfNull(id);

        if (id == Guid.Empty)
        {
            throw new DomainException("Order id is required.");
        }

        return new OrderId(id);
    }
}
