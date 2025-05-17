using Ecommerce.Payment.Domain.TransactionAggregate;

namespace Ecommerce.Payment.Domain.OrderAggregate;

public class Order : BaseEntity
{
    public Order(
        Guid customerId,
        string? addressFullName,
        string addressPhone,
        string addressRegion,
        string addressCity,
        string addressStreet,
        string addressZipCode)
    {
        CustomerId = customerId;
        Status = OrderStatus.Pending;
        ShippingAmount = 0;
        TotalPrice = 0;
        TotalQuantity = 0;
        Address = new OrderAddress
        {
            FullName = addressFullName,
            Phone = addressPhone,
            Region = addressRegion,
            City = addressCity,
            Street = addressStreet,
            ZipCode = addressZipCode
        };
        Items = [];
    }
    
    private Order() { }
    
    public long Id { get; private set; }
    
    public Guid CustomerId { get; private set; }
    
    public OrderStatus Status { get; private set; }
    
    public decimal ShippingAmount { get; private set; }
    
    public decimal TotalPrice { get; private set; }
    
    public decimal TotalQuantity { get; private set; }
    
    public OrderAddress Address { get; private set; }
    
    public Transaction? Transaction { get; private set; }
    
    public ICollection<OrderItem> Items { get; private set; }

    public void AddItem(OrderItem item)
    {
        Items.Add(item);
        
        Calculate();
    }

    public void Calculate()
    {
        TotalPrice = Items.Sum(i => i.Quantity * i.UnitPrice);
        TotalQuantity = Items.Sum(i => i.Quantity);
    }
    
    public void SetStatus(OrderStatus status) => Status = status;
}