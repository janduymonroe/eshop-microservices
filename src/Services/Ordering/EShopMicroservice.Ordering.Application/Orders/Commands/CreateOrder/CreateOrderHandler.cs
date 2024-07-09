namespace EShopMicroservice.Ordering.Application.Orders.Commands.CreateOrder;

public sealed class CreateOrderHandler(IApplicationDbContext dbContext) 
    : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = CreateNewOrder(command.Order);

        dbContext.Orders.Add(order);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateOrderResult(order.Id.Value);
    }

    private Order CreateNewOrder(OrderDto orderDto)
    {
        var order = Order.Create(
                id: OrderId.Of(Guid.NewGuid()),
                customerId: CustomerId.Of(orderDto.CustomerId),
                orderName: OrderName.Of(orderDto.OrderName),
                shippingAddress: orderDto.ShippingAddress.ToValueObject(),
                billingAddress: orderDto.BillingAddress.ToValueObject(),
                payment: orderDto.Payment.ToValueObject()
            );

        orderDto.OrderItems.ForEach(x => order.AddOrderItem(ProductId.Of(x.ProductId), x.Quantity, x.Price));

        return order;
    }
}
