namespace Ecommerce.Payment.Domain.OrderAggregate;

public enum OrderStatus
{
    Pending,
    Paid,
    PaymentFailed,
    Shipped,
    Delivered
}