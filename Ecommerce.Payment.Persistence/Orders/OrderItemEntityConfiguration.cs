using Ecommerce.Payment.Domain.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Payment.Persistence.Orders;

public class OrderItemEntityConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("order_items");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired();
        
        builder.Property(x => x.ProductId)
            .IsRequired();
        
        builder.Property(x => x.OrderId)
            .IsRequired();
        
        builder.Property(x => x.UnitPrice)
            .IsRequired();
        
        builder.Property(x => x.Quantity)
            .IsRequired();
        
        builder.HasOne(x => x.Product)
            .WithMany(x => x.OrderItems)
            .HasForeignKey(x => x.ProductId);
        
        builder.HasOne(x => x.Order)
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.OrderId);
    }
}