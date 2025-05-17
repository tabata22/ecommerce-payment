using CSharpFunctionalExtensions;

namespace Ecommerce.Payment.Application.CatalogService;

public interface ICatalogService
{
    Task<Result> LockProductAsync(LockProductRequest request, CancellationToken cancellationToken = default);
}