namespace Ecommerce.Payment.Domain.CardAggregate;

public class Card : BaseEntity
{
    public Card(
        Guid id, 
        Guid customerId, 
        string number, 
        DateOnly expirationDate,
        CardType type)
    {
        Id = id;
        CustomerId = customerId;
        Number = number;
        ExpirationDate = expirationDate;
        Type = type;
    }
    
    private Card() { }

    public Guid Id { get; private set; }
    
    public Guid CustomerId { get; private set; }
    
    public string Number { get; private set; }
    
    public DateOnly ExpirationDate { get; private set; }
    
    public CardType Type { get; private set; }
}