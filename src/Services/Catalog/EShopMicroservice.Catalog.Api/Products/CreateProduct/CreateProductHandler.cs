using BuldingBlocks.CQRS;
using EShopMicroservice.Catalog.Api.Models;

namespace EShopMicroservice.Catalog.Api.Products.CreateProduct;

public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<CreateProductResult>
{
    public Product ToModel() => new Product
    {
        Name = Name,
        Category = Category,
        Description = Description,
        ImageFile = ImageFile,
        Price = Price
    };
};
public record CreateProductResult(Guid Id);

internal sealed class CreateProductCommandHandler(IDocumentSession session) 
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        // Business logic to create a product.
        var product = command.ToModel();

        session.Store(product);

        await session.SaveChangesAsync(cancellationToken);

        return new CreateProductResult(Guid.NewGuid());
    }
}
