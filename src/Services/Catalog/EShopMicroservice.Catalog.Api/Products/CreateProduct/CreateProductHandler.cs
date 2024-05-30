﻿

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

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be more than 0");
    }
}

internal sealed class CreateProductCommandHandler
    (IDocumentSession session) 
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
