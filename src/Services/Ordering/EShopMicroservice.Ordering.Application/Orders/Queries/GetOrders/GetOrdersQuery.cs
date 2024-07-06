using BuldingBlocks.Pagination;

namespace EShopMicroservice.Ordering.Application.Orders.Queries.GetOrders;

public record GetOrdersQuery(PaginationRequest PaginationRequest)
    : IQuery<GetOrdersResult>;

public record GetOrdersResult(PaginationResult<OrderDto> Orders);