namespace Ecommerce.Payment.Domain.OrderAggregate;

public enum OrderStatus
{
    Pending = 0,
    Paid = 5,
    PaymentFailed = 10,
    Shipped = 15,
    Delivered = 20
}