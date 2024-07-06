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
}
