namespace EShopMicroservice.Ordering.Domain.ValueObject;

public class ProductId : IEntityId
{
    public Guid Value { get; }
    private ProductId(Guid value) => Value = value;

    public static ProductId Of(Guid id)
    {
        ArgumentNullException.ThrowIfNull(id);

        if (id == Guid.Empty)
        {
            throw new DomainException("Product id is required.");
        }

        return new ProductId(id);
    }
}
