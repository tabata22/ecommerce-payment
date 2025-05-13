using System.Text.Json.Serialization;

namespace Ecommerce.Payment.Application.PaymentProviders.Requests;

public class PayByCardRequest
{
    [JsonIgnore]
    public Guid IdempotencyKey { get; set; }
    
    public string OrderId { get; set; }
    
    [JsonPropertyName("callback_url")]
    public string CallbackUrl { get; set; }
    
    [JsonPropertyName("purchase_units")]
    public PurchaseUnit PurchaseUnit { get; set; }
}