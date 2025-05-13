using Ecommerce.Payment.Domain.OrderAggregate;

namespace Ecommerce.Payment.Application.Orders.Queries;

public interface IOrderQueries
{
    Task<IReadOnlyList<Order>> GetOrders(Guid customerId, CancellationToken cancellationToken = default);
    
    Task<Order?> GetInvoice(Guid customerId, long orderId, CancellationToken cancellationToken = default);
}