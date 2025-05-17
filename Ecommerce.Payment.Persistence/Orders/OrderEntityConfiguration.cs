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
        
        builder.OwnsOne(x => x.Address, sa =>
        {
            sa.Property(a => a.FullName).HasColumnName("address_full_name");
            sa.Property(a => a.Phone).HasColumnName("address_phone");
            sa.Property(a => a.Region).HasColumnName("address_region");
            sa.Property(a => a.City).HasColumnName("address_city");
            sa.Property(a => a.Street).HasColumnName("address_street");
            sa.Property(a => a.ZipCode).HasColumnName("address_zip_code");

            sa.ToJson();
        });
        
        builder.HasMany(x => x.Items)
            .WithOne(x => x.Order)
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}