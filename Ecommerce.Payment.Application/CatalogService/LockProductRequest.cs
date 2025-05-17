namespace Ecommerce.Payment.Application.CatalogService;

public record LockProductRequest(long ProductId, int Quantity);