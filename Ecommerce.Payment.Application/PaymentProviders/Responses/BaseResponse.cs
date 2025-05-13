using System.Text.Json.Serialization;

namespace Ecommerce.Payment.Application.PaymentProviders.Responses;

public class BaseResponse
{
    [JsonIgnore]
    public bool IsSuccess { get; set; }
    
    [JsonIgnore]
    public string ErrorMessage { get; set; }
    
    public static BaseResponse Success() => new() { IsSuccess = true };
}