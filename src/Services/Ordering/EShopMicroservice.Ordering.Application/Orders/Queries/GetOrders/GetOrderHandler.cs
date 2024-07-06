
using BuldingBlocks.Pagination;

namespace EShopMicroservice.Ordering.Application.Orders.Queries.GetOrders;

public class GetOrderHandler
    (IApplicationDbContext dbContext)
    : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    public async Task<GetOrdersResult> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var pageSize = request.PaginationRequest.PageSize;
        var pageIndex = request.PaginationRequest.PageIndex;

        var totalCount = await dbContext.Orders
                                        .LongCountAsync(cancellationToken);

        var orders = await dbContext.Orders
                                    .Include(o => o.OrderItems)
                                    .AsNoTracking()
                                    .Skip(pageIndex * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync(cancellationToken);

        var result = new PaginationResult<OrderDto>
            (
                pageIndex,
                pageSize,
                totalCount,
                orders.Select(OrderDto.FromOrder)
            );

        return new GetOrdersResult(result);
    }
}
