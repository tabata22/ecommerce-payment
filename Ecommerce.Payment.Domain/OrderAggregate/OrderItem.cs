using Ecommerce.Payment.Domain.ProductAggregate;

namespace Ecommerce.Payment.Domain.OrderAggregate;

public class OrderItem : BaseEntity
{
    public OrderItem(long productId, long orderId, decimal unitPrice, int quantity)
    {
        Id = Guid.NewGuid();
        ProductId = productId;
        OrderId = orderId;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }
    
    private OrderItem() { }

    public Guid Id { get; private set; }
    
    public long ProductId { get; private set; }
    
    public long OrderId { get; private set; }
    
    public decimal UnitPrice { get; private set; }
    
    public int Quantity { get; private set; }
    
    public Order Order { get; private set; }
    
    public Product Product { get; private set; }
}