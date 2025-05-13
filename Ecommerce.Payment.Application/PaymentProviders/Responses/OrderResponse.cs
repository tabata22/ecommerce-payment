using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Ecommerce.Payment.Application.PaymentProviders.Responses;

public class OrderResponse : BaseResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    
    [JsonPropertyName("_links")]
    public Link Links { get; set; }
    
    [DataContract]
    public class Link
    {
        [JsonPropertyName("redirect")]
        public Redirect Redirect { get; set; }
    }

    [DataContract]
    public class Redirect
    {
        [JsonPropertyName("href")]
        public string Href { get; set; }
    }
}