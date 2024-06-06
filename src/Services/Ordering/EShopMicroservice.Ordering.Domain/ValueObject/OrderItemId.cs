namespace EShopMicroservice.Ordering.Domain.ValueObject;

public record OrderItemId : IEntityId
{
    public Guid Value { get; }
    private OrderItemId(Guid value) => Value = value;

    public static OrderItemId Of(Guid id)
    {
        ArgumentNullException.ThrowIfNull(id);

        if (id == Guid.Empty)
        {
            throw new DomainException("Order item id is required.");
        }

        return new OrderItemId(id);
    }
}
