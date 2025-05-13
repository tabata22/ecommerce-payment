using System.Text.Json.Serialization;
using Ecommerce.Payment.Domain.CardAggregate;
using Ecommerce.Payment.Domain.TransactionAggregate;

namespace Ecommerce.Payment.Api.Requests;

public class FinishTransactionRequest
{
    [JsonPropertyName("event")]
    public string Event { get; set; }
    
    [JsonPropertyName("zoned_request_time")]
    public DateTimeOffset RequestTime { get; set; }
    
    [JsonPropertyName("body")]
    public FinishTransactionBodyDto Body { get; set; }
}

public class FinishTransactionBodyDto
{
    [JsonPropertyName("order_id")]
    public string OrderId { get; set; }
    
    [JsonPropertyName("order_status")]
    public OrderStatusDto OrderStatus { get; set; }
    
    [JsonPropertyName("payment_detail")]
    public PaymentDetailDto PaymentDetails { get; set; }
    
    [JsonPropertyName("reject_reason")]
    public string? RejectReason { get; set; }
}

public class OrderStatusDto
{
    [JsonPropertyName("key")]
    public string Key { get; set; }
    
    [JsonPropertyName("value")]
    public string Value { get; set; }
}

public class PaymentDetailDto
{
    [JsonPropertyName("code")] public int Code { get; set; }

    [JsonPropertyName("code_description")] public string CodeDescription { get; set; }

    [JsonPropertyName("transaction_id")] public string TransactionId { get; set; }

    [JsonPropertyName("payer_identifier")] public string PayerIdentifier { get; set; }

    [JsonPropertyName("card_type")] public string CardTypeBank { get; set; }

    [JsonPropertyName("card_expiry_date")] public string CardExpiration { get; set; }

    [JsonPropertyName("saved_card_type")] public string SavedCardType { get; set; }

    [JsonIgnore] public DateOnly CardExpiryDate => DateOnly.Parse($"01/{CardExpiration}");

    public CardType CardType => CardTypeBank switch
    {
        "mc" => CardType.MasterCard,
        "visa" => CardType.Visa,
        _ => throw new ArgumentOutOfRangeException()
    };

    public TransactionStatus Status => Code switch 
    {
        100 => TransactionStatus.Completed,
        _ => TransactionStatus.Failed
    };
}