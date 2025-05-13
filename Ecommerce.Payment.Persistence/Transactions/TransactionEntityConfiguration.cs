using Ecommerce.Payment.Domain.TransactionAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Payment.Persistence.Transactions;

public class TransactionEntityConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("transactions");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(x => x.OrderId)
            .IsRequired();
        
        builder.Property(x => x.CustomerId)
            .IsRequired();
        
        builder.Property(x => x.ProviderId)
            .IsRequired();
        
        builder.Property(x => x.PaymentMethod)
            .IsRequired();
        
        builder.Property(x => x.Status)
            .IsRequired();
        
        builder.Property(x => x.Amount)
            .IsRequired();
        
        builder.Property(x => x.FinishDate)
            .IsRequired(false);
        
        builder.HasOne(x => x.Order)
            .WithOne(x => x.Transaction)
            .HasForeignKey<Transaction>(x => x.OrderId);

        builder.HasIndex(x => x.ProviderId);
    }
}