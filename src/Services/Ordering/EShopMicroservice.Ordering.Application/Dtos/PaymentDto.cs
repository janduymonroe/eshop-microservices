namespace EShopMicroservice.Ordering.Application.Dtos;

public record PaymentDto(
    string CardHolderName,
    string CardNumber,
    string CardExpiration,
    string CardSecurityNumber,
    int PaymentMethod
    )
{
    public Payment ToValueObject()
    {
        return Payment.Of(CardHolderName, CardNumber, CardExpiration, CardSecurityNumber, PaymentMethod);
    }

    public static PaymentDto FromPayment(Payment payment)
    {
        return new PaymentDto(
            payment.CardHolderName,
            payment.CardNumber,
            payment.CardExpiration,
            payment.CardSecurityNumber,
            payment.PaymentMethod
        );
    }

    public static PaymentDto FromBasketCheckoutEvent(BasketCheckoutEvent @event)
    {
        return new PaymentDto(
            @event.CardHolderName,
            @event.CardNumber,
            @event.CardExpiration,
            @event.CardSecurityNumber,
            @event.PaymentMethod
        );
    }
}

