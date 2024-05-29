
namespace EShopMicroservice.Catalog.Api.Products.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price) 
    :ICommand<UpdateProductResult>;

public record UpdateProductResult(bool IsSuccess);

public class UpdateProductHandler
    (IDocumentSession session, ILogger<UpdateProductHandler> logger)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("UpdateProductHandler.Handle called with {@Command}", command);

        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

        if (product == null)
        {
            return new UpdateProductResult(false);
        }

        product.Update(
            command.Name, 
            command.Category, 
            command.Description, 
            command.ImageFile, 
            command.Price);

        session.Update(product);

        await session.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(true);
    }
}
