using Ecommerce.Payment.Domain.CardAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Payment.Persistence.Cards;

public class CardEntityConfiguration : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder.ToTable("cards");
        
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedNever();
        
        builder.Property(c => c.CustomerId)
            .IsRequired();
        
        builder.Property(c => c.Number)
            .IsRequired();
        
        builder.Property(c => c.ExpirationDate)
            .IsRequired();
        
        builder.Property(c => c.Type)
            .IsRequired();

        builder.HasIndex(x => x.CustomerId);
    }
}