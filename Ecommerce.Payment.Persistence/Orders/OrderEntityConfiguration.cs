using Ecommerce.Payment.Domain.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Payment.Persistence.Orders;

public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();
        
        builder.Property(x => x.CustomerId)
            .IsRequired();
        
        builder.Property(x => x.ShippingAmount)
            .IsRequired();
        
        builder.Property(x => x.TotalPrice)
            .IsRequired();
        
        builder.Property(x => x.TotalQuantity)
            .IsRequired();
    }
}