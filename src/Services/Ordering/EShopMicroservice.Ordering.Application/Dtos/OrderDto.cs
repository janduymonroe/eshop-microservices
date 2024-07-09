using EShopMicroservice.Ordering.Domain.Enum;

namespace EShopMicroservice.Ordering.Application.Dtos;

public record OrderDto(
    Guid Id,
    Guid CustomerId,
    string OrderName,
    AddressDto ShippingAddress,
    AddressDto BillingAddress,
    PaymentDto Payment,
    OrderStatus Status,
    List<OrderItemDto> OrderItems)
{
    public static OrderDto FromOrder(Order order)
    {
        return new OrderDto(
            order.Id.Value,
            order.CustomerId.Value,
            order.OrderName.Value,
            AddressDto.FromAddress(order.ShippingAddress),
            AddressDto.FromAddress(order.BillingAddress),
            PaymentDto.FromPayment(order.Payment),
            order.Status,
            order.OrderItems.Select(OrderItemDto.FromOrder).ToList()
        );
    }

    public static OrderDto FromBasketCheckountEvent(BasketCheckoutEvent @event)
    {
        return new OrderDto(
            @event.Id,
            @event.CustomerId,
            @event.UserName,
            AddressDto.FromBasketCheckoutEvent(@event),
            AddressDto.FromBasketCheckoutEvent(@event),
            PaymentDto.FromBasketCheckoutEvent(@event),
            OrderStatus.Pending,
            [new OrderItemDto(@event.Id, new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"), 2, 500), 
             new OrderItemDto(@event.Id, new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914"), 1, 400)]
            );
    }
}
