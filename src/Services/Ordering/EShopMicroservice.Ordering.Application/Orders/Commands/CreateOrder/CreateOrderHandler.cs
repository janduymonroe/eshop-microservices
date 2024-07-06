using EShopMicroservice.Ordering.Application.Dtos;


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

    private Order CreateNewOrder(OrderDto order)
    {
        return Order.Create(
                id: OrderId.Of(Guid.NewGuid()),
                customerId: CustomerId.Of(order.CustomerId),
                orderName: OrderName.Of(order.OrderName),
                shippingAddress: order.ShippingAddress.ToValueObject(),
                billingAddress: order.BillingAddress.ToValueObject(),
                payment: order.Payment.ToValueObject()
            );
    }
}
