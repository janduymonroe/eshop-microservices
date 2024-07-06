namespace EShopMicroservice.Ordering.Application.Dtos;

public record OrderItemDto(
    Guid OrderId,
    Guid ProductId,
    int Quantity,
    decimal Price
    )
{
    public static OrderItemDto FromOrder(OrderItem orderItem)
    {
        return new OrderItemDto(orderItem.OrderId.Value, orderItem.ProductId.Value, orderItem.Quantity, orderItem.Price);
    }
}
