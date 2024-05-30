﻿
namespace EShopMicroservice.Catalog.Api.Products.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

public record GetProductByIdResult(Product Product);

internal class GetProductByIdHandler(IDocumentSession session)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await  session.LoadAsync<Product>(query.Id);

        if(product is null)
        {
            throw new ProductNotFoundException(query.Id);
        }

        return new GetProductByIdResult(product);
    }
}

