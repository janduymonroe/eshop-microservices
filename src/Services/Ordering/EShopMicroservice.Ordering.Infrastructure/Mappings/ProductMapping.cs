﻿namespace EShopMicroservice.Ordering.Infrastructure.Mappings;

public class ProductMapping : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasConversion(
            productId => productId.Value,
            dbId => ProductId.Of(dbId));

        builder.Property(p => p.Name)
            .HasMaxLength(100)
            .IsRequired();
    }
}
