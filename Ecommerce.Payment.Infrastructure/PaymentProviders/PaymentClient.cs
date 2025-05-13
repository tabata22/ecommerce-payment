using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using Ecommerce.Payment.Application.PaymentProviders;
using Ecommerce.Payment.Application.PaymentProviders.Requests;
using Ecommerce.Payment.Application.PaymentProviders.Responses;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Payment.Infrastructure.PaymentProviders;

public class PaymentClient : IPaymentClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IMemoryCache _memoryCache;
    private readonly ILogger<PaymentClient> _logger;
    private readonly BogPaymentSettings _bogPaymentSettings;
    
    private readonly SemaphoreSlim _semaphoreSlim = new(1, 1);
    private const string TokenCacheKey = "BogPaymentToken";

    public PaymentClient(
        IHttpClientFactory httpClientFactory, 
        IMemoryCache memoryCache, 
        ILogger<PaymentClient> logger, 
        BogPaymentSettings bogPaymentSettings)
    {
        _httpClientFactory = httpClientFactory;
        _memoryCache = memoryCache;
        _logger = logger;
        _bogPaymentSettings = bogPaymentSettings;
    }

    public async Task<OrderResponse> OrderAsync(OrderRequest request, CancellationToken cancellationToken = default)
    { 
        var token = await GetTokenAsync(cancellationToken);
        
        var httpClient = _httpClientFactory.CreateClient("BogPayment")
            .WithAuthorization(token)
            .WithIdempotency(request.IdempotencyKey);
        
        try
        {
            using var response = await httpClient.PostAsJsonAsync("ecommerce/orders", request, cancellationToken);
           
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<OrderResponse>(cancellationToken);
            result!.IsSuccess = true;
            
            return result;
        }
        catch (Exception e)
        {
            return new OrderResponse
            {
                IsSuccess = false, 
                ErrorMessage = e.Message
            };
        }
    }

    public async Task<BaseResponse> SaveCardAsync(Guid orderId, CancellationToken cancellationToken = default)
    {
        var token =  await GetTokenAsync(cancellationToken);
        var httpClient = _httpClientFactory
            .CreateClient("BogPayment")
            .WithAuthorization(token);
        
        try
        {
            using var response = await httpClient.PutAsync($"orders/{orderId}/cards", null, cancellationToken);
            
            response.EnsureSuccessStatusCode();

            return BaseResponse.Success();
        }
        catch (Exception e)
        {
            return new OrderResponse
            {
                IsSuccess = false, 
                ErrorMessage = e.Message
            };
        }
    }

    public async Task<BaseResponse> PayByCardAsync(PayByCardRequest request, CancellationToken cancellationToken = default)
    {
        var token =  await GetTokenAsync(cancellationToken); 
        var httpClient = _httpClientFactory.CreateClient("BogPayment")
            .WithAuthorization(token)
            .WithIdempotency(request.IdempotencyKey);
        
        try
        {
            using var response = await httpClient.PostAsJsonAsync($"orders/{request.OrderId}/cards", request, cancellationToken);
            
            response.EnsureSuccessStatusCode();

            return BaseResponse.Success();
        }
        catch (Exception e)
        {
            return new OrderResponse
            {
                IsSuccess = false, 
                ErrorMessage = e.Message
            };
        }
    }

    public async Task<BaseResponse> RefundAsync(RefundRequest request, CancellationToken cancellationToken = default)
    {
        var token = await GetTokenAsync(cancellationToken);
        var httpClient = _httpClientFactory.CreateClient("BogPayment")
            .WithAuthorization(token)
            .WithIdempotency(request.IdempotencyKey);
        
        try
        {
            using var response = await httpClient.PostAsJsonAsync($"payment/refund/{request.OrderId}", request, cancellationToken);
            
            response.EnsureSuccessStatusCode();

            return BaseResponse.Success();
        }
        catch (Exception e)
        {
            return new OrderResponse
            {
                IsSuccess = false, 
                ErrorMessage = e.Message
            };
        }
    }

    private async Task<BearerTokenResponse> GetTokenAsync(CancellationToken cancellationToken)
    {
        await _semaphoreSlim.WaitAsync(cancellationToken);

        try
        {
            if (_memoryCache.TryGetValue(TokenCacheKey, out BearerTokenResponse token))
                return token;

            using var httpClient = _httpClientFactory.CreateClient();

            var secretsArray = Encoding.UTF8.GetBytes($"{_bogPaymentSettings.ClientId}:{_bogPaymentSettings.SecretKey}");
            var base64 = Convert.ToBase64String(secretsArray);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

            using var postData = new FormUrlEncodedContent([
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            ]);

            using var response = await httpClient.PostAsync(new Uri(_bogPaymentSettings.AuthPath), postData, cancellationToken);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<BearerTokenResponse>(cancellationToken: cancellationToken);
            if (result is null)
                throw new InvalidOperationException("Invalid token response received");

            _memoryCache.Set(TokenCacheKey, result, TimeSpan.FromSeconds(result.ExpiresIn - 5));

            return result;
        }
        catch (Exception e)
        {
            _logger.LogError("error while getting authorization token. {ErrorMessage}", e.Message);

            throw;
        }
        finally
        {
            _semaphoreSlim.Release();
        }
    }
    
    public void ValidateSignature(string signature, string data)
    {
        var signatureBytes = Convert.FromBase64String(signature);
        var dataBytes = Encoding.UTF8.GetBytes(data);
        
        using var rsa = RSA.Create();
        rsa.ImportFromPem(_bogPaymentSettings.PublicKey);
        
        var result = rsa.VerifyData(dataBytes, signatureBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        if (!result)
            throw new SecurityException("Payment signature verification failed.");
    }
}