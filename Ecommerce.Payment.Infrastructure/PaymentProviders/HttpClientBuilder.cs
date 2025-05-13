using System.Net.Http.Headers;

namespace Ecommerce.Payment.Infrastructure.PaymentProviders;

internal static class HttpClientBuilder
{
    public static HttpClient WithAuthorization(this HttpClient httpClient, BearerTokenResponse token)
    {
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
            token.TokenType,
            token.AccessToken);
        
        return httpClient;
    }

    public static HttpClient WithIdempotency(this HttpClient httpClient, Guid idempotencyKey)
    {
        httpClient.DefaultRequestHeaders.Add("Idempotency-Key", idempotencyKey.ToString());
        
        return httpClient;
    }
}