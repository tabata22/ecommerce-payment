using Ecommerce.Payment.Domain.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Payment.Persistence.Products;

public class ProductEntityConfiguration :  IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");
        
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedNever();
        
        builder.Property(p => p.Name)
            .IsRequired();
        
        builder.Property(p => p.Price)
            .IsRequired();

        builder.Property(p => p.Image)
            .IsRequired();
    }
}