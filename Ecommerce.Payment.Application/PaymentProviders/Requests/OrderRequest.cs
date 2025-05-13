using System.Text.Json.Serialization;

namespace Ecommerce.Payment.Application.PaymentProviders.Requests;

public class OrderRequest
{
    [JsonIgnore]
    public Guid IdempotencyKey { get; set; }
    
    [JsonPropertyName("callback_url")]
    public string CallbackUrl { get; init; }
    
    [JsonPropertyName("purchase_units")] 
    public PurchaseUnit PurchaseUnit { get; init; }
}

public class OrderByCardRequest
{
    [JsonIgnore]
    public Guid IdempotencyKey { get; set; }
    
    [JsonIgnore]
    public string OrderId { get; init; }
    
    [JsonPropertyName("purchase_units")] 
    public PurchaseUnit PurchaseUnit { get; init; }
}

public class PurchaseUnit
{
    [JsonPropertyName("currency")]
    public string Currency { get; init; }
    
    [JsonPropertyName("total_amount")]
    public decimal TotalAmount { get; init; }
    
    [JsonPropertyName("basket")]
    public List<Basket> Basket { get; init; }
}

public class Basket
{
    [JsonPropertyName("quantity")]
    public int Quantity { get; init; }
    
    [JsonPropertyName("unit_price")]
    public decimal UnitPrice { get; init; }
    
    [JsonPropertyName("product_id")]
    public string ProductId { get; init; }
}