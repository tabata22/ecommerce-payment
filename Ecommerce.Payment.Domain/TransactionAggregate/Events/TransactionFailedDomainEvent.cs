namespace Ecommerce.Payment.Domain.TransactionAggregate.Events;

public record TransactionFailedDomainEvent(long TransactionId, long OrderId) : IDomainEvent;