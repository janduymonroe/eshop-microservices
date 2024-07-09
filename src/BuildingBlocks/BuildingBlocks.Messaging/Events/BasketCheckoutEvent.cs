﻿namespace BuildingBlocks.Messaging.Events;

public record BasketCheckoutEvent : IntegrationEvent
{
    public string UserName { get; set; } = default!;
    public Guid CustomerId { get; set; } = default!;
    public decimal TotalPrice { get; set; } = default!;

    // Shipping and BillingAddress
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string EmailAddress { get; set; } = default!;
    public string AddressLine { get; set; } = default!;
    public string Country { get; set; } = default!;
    public string State { get; set; } = default!;
    public string ZipCode { get; set; } = default!;

    // Payment
    public string CardHolderName { get; set; } = default!;
    public string CardNumber { get; set; } = default!;
    public string CardExpiration { get; set; } = default!;
    public string CardSecurityNumber { get; set; } = default!;
    public int PaymentMethod { get; set; } = default!;
}
