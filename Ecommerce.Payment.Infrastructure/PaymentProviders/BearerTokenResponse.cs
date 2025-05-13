using System.Text.Json.Serialization;

namespace Ecommerce.Payment.Infrastructure.PaymentProviders;

public class BearerTokenResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }
    
    [JsonPropertyName("token_type")]
    public string TokenType { get; set; }

    [JsonPropertyName("expires_in")]
    public double ExpiresIn { get; set; }
}