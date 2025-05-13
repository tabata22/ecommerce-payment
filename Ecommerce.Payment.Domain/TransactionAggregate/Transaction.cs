using Ecommerce.Payment.Domain.CardAggregate;
using Ecommerce.Payment.Domain.OrderAggregate;
using Ecommerce.Payment.Domain.TransactionAggregate.Events;

namespace Ecommerce.Payment.Domain.TransactionAggregate;

public class Transaction : BaseEntity
{
    public Transaction(
        long orderId,
        Guid customerId,
        Guid providerId,
        PaymentMethod paymentMethod,
        decimal amount)
    {
        OrderId = orderId;
        CustomerId = customerId;
        ProviderId = providerId;
        PaymentMethod = paymentMethod;
        Status = TransactionStatus.Pending;
        Amount = amount;
    }
    
    private Transaction() { }

    public long Id { get; private set; }
    
    public long OrderId { get; private set; }
    
    public Guid CustomerId { get; private set; }
    
    public Guid ProviderId { get; private set; }
    
    public PaymentMethod PaymentMethod { get; private set; }
    
    public TransactionStatus Status { get; private set; }
    
    public decimal Amount { get; private set; }
    
    public DateTimeOffset? FinishDate { get; private set; }
    
    public Order Order { get; private set; }
    
    public void Refund() => Status = TransactionStatus.Refunded;
    
    public void Complete(string cardNumber, DateOnly cardExpiration, CardType cardType)
    {
        Status = TransactionStatus.Completed;
        FinishDate = DateTimeOffset.UtcNow;
        
        RaiseEvent(new TransactionCompletedDomainEvent(
            Id, 
            OrderId, 
            CustomerId,
            ProviderId,
            cardNumber, 
            cardExpiration, 
            cardType));
    }
    
    public void Fail()
    {
        Status = TransactionStatus.Failed;
        FinishDate = DateTimeOffset.UtcNow;
        
        RaiseEvent(new TransactionFailedDomainEvent(Id, OrderId));
    }
}