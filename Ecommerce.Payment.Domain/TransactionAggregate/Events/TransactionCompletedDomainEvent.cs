using Ecommerce.Payment.Domain.CardAggregate;

namespace Ecommerce.Payment.Domain.TransactionAggregate.Events;

public record TransactionCompletedDomainEvent(
    long TransactionId,
    long OrderId,
    Guid CustomerId,
    Guid ProviderId,
    string CardNumber,
    DateOnly CardExpiration,
    CardType CardType) : IDomainEvent;