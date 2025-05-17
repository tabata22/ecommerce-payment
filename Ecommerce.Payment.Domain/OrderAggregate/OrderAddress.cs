namespace Ecommerce.Payment.Domain.OrderAggregate;

public class OrderAddress
{
    public string? FullName { get; set; }
    
    public string Phone { get; set; }
    
    public string Region { get; set; }
    
    public string City { get; set; }
    
    public string Street { get; set; }
    
    public string ZipCode { get; set; }
}