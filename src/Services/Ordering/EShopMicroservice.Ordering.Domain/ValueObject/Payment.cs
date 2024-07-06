namespace EShopMicroservice.Ordering.Domain.ValueObject;

public record Payment
{
    public string CardHolderName { get; } = default!;
    public string CardNumber { get; } = default!;
    public string CardExpiration { get; } = default!;
    public string CardSecurityNumber { get; } = default!;
    public int PaymentMethod { get; } = default!;

    protected Payment() { }

    private Payment(string cardHolderName, string cardNumber, string cardExpiration, string cardSecurityNumber, int paymentMethod)
    {
        CardHolderName = cardHolderName;
        CardNumber = cardNumber;
        CardExpiration = cardExpiration;
        CardSecurityNumber = cardSecurityNumber;
        PaymentMethod = paymentMethod;
    }

    public static Payment Of(string cardHolderName, string cardNumber, string cardExpiration, string cardSecurityNumber, int paymentMethod)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(cardHolderName, nameof(cardHolderName));
        ArgumentException.ThrowIfNullOrWhiteSpace(cardNumber, nameof(cardNumber));
        ArgumentException.ThrowIfNullOrWhiteSpace(cardSecurityNumber, nameof(cardSecurityNumber));
        ArgumentOutOfRangeException.ThrowIfGreaterThan(cardSecurityNumber.Length, 3);

        return new Payment(cardHolderName, cardNumber, cardExpiration, cardSecurityNumber, paymentMethod);
    }
}
