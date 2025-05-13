using Ecommerce.Payment.Domain.OrderAggregate;

namespace Ecommerce.Payment.Domain.ProductAggregate;

public class Product : BaseEntity
{
    public Product(long id, string name, decimal price, string image)
    {
        Id = id;
        Name = name;
        Price = price;
        Image = image;
    }
    
    private Product() { }

    public long Id { get; private set; }
    
    public string Name { get; private set; }
    
    public decimal Price { get; private set; }
    
    public string Image { get; private set; }
    
    public ICollection<OrderItem> OrderItems { get; private set; }

    public void Update(string name, decimal price, string image)
    {
        Name = name;
        Price = price;
        Image = image;
    }
}