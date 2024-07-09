namespace EShopMicroservice.Ordering.Domain.ValueObject;

public record OrderName
{
    private const int MaxLength = 3;
    public string Value { get; }
    private OrderName(string value) => Value = value;

    public static OrderName Of(string value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value, nameof(value));
        ArgumentOutOfRangeException.ThrowIfNotEqual(value.Length, MaxLength, nameof(value));

        return new OrderName(value);
    }
}
