using Ecommerce.Payment.Domain.CardAggregate;

namespace Ecommerce.Payment.Application.Cards.Queries;

public class GetCardsDto
{
    public Guid Id { get; set; }
    
    public string Number { get; set; }
    
    public CardType Type { get; set; }
    
    public bool IsExpired { get; set; }
}