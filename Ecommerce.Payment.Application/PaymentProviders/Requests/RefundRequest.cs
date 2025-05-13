using System.Text.Json.Serialization;

namespace Ecommerce.Payment.Application.PaymentProviders.Requests;

public class RefundRequest
{
    [JsonIgnore]
    public Guid IdempotencyKey { get; set; }
    
    [JsonIgnore]
    public Guid OrderId { get; init; }
    
    // [JsonPropertyName("amount")]
    // public decimal Amount { get; init; }
}