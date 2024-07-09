using EShopMicroservice.Ordering.Application.Orders.Commands.CreateOrder;

namespace EShopMicroservice.Ordering.Application.Orders.EventHandlers.Integration;

public class BasketCheckoutEventHandler
    (ISender sender, ILogger<BasketCheckoutEventHandler> logger)
    : IConsumer<BasketCheckoutEvent>
{
    public async Task Consume
        (ConsumeContext<BasketCheckoutEvent> context)
    {
        logger.LogInformation("----- Handling integration event: {IntegrationEventId} ({IntegrationEvent})", context.Message.Id, context.Message.GetType().Name);

        var command = MapToCreateOrderCommand(context.Message);
        await sender.Send(command);
    }

    private CreateOrderCommand MapToCreateOrderCommand
        (BasketCheckoutEvent basketCheckoutEvent) =>
        new CreateOrderCommand(OrderDto.FromBasketCheckountEvent(basketCheckoutEvent));
}
