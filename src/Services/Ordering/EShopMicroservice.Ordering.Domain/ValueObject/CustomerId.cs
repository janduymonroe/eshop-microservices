namespace EShopMicroservice.Ordering.Domain.ValueObject;

public record CustomerId : IEntityId
{
    public Guid Value { get; }
    private CustomerId(Guid value) => Value = value;
    public static CustomerId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);

        if (value == Guid.Empty)
            throw new DomainException("CustomerId cannot be empty");

        return new CustomerId(value);
    }
}
