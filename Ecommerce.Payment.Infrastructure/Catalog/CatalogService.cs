using System.Net;
using System.Net.Http.Json;
using CSharpFunctionalExtensions;
using Ecommerce.Payment.Application.CatalogService;

namespace Ecommerce.Payment.Infrastructure.Catalog;

public class CatalogService : ICatalogService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CatalogService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<Result> LockProductAsync(LockProductRequest request, CancellationToken cancellationToken = default)
    {
        var client = _httpClientFactory.CreateClient("CatalogService");
        
        const string path = "products/lock";
        using var response = await client.PostAsJsonAsync(path, request, cancellationToken);
        
        if (response.StatusCode == HttpStatusCode.InternalServerError)
        {
            return Result.Failure("Error while locking product");
        }

        return await response.Content.ReadFromJsonAsync<Result>(cancellationToken);
    }
}