namespace Ecommerce.Payment.Domain.TransactionAggregate;

public enum TransactionStatus
{
    Pending = 0,
    Completed = 5,
    Refunded = 98,
    Failed = 99,
}