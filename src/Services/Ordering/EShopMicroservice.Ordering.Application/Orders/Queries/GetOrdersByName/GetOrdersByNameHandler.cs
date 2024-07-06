
namespace EShopMicroservice.Ordering.Application.Orders.Queries.GetOrdersByName;

public class GetOrdersByNameHandler
    (IApplicationDbContext dbContext)
    : IQueryHandler<GetOrdersByName, GetOrdersByNameResult>
{
    public async Task<GetOrdersByNameResult> Handle(GetOrdersByName query, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
                .Include(x => x.OrderItems)
                .AsNoTracking()
                .Where(x => x.OrderName.Value.Contains(query.Name))
                .OrderBy(x => x.OrderName)
                .ToListAsync(cancellationToken);

        var orderDtos = ProjectToOrderDto(orders);

        return new GetOrdersByNameResult(orderDtos);
    }

    private IEnumerable<OrderDto> ProjectToOrderDto(IEnumerable<Order> orders)
    {
        return orders.Select(OrderDto.FromOrder).ToList();
    }
}
