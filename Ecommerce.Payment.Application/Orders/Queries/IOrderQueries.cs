using Ecommerce.Payment.Domain.OrderAggregate;

namespace Ecommerce.Payment.Application.Orders.Queries;

public interface IOrderQueries
{
    Task<IReadOnlyList<Order>> GetOrdersAsync(Guid customerId, CancellationToken cancellationToken = default);
    
    Task<Order?> GetInvoiceAsync(Guid customerId, long orderId, CancellationToken cancellationToken = default);
}