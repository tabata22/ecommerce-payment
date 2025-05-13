namespace Ecommerce.Payment.Application.PaymentProviders;

public class BogPaymentSettings
{
    public const string SettingsKey = "BogPaymentSettings";
    
    public string ClientId { get; set; }
    
    public string SecretKey { get; set; }
    
    public string BasePath { get; set; }
    
    public string AuthPath { get; set; }
    
    public string PublicKey { get; set; }
    
    public string CallbackUrl { get; set; }
}