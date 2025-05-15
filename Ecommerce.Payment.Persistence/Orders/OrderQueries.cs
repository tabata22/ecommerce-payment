using Ecommerce.Payment.Application.Orders.Queries;
using Ecommerce.Payment.Domain.OrderAggregate;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Payment.Persistence.Orders;

public class OrderQueries :  IOrderQueries
{
    private readonly ApplicationDbContext _dbContext;

    public OrderQueries(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<Order>> GetOrdersAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Orders
            .AsNoTracking()
            .Include(x => x.Items)
            .Where(x => x.CustomerId == customerId)
            .ToListAsync(cancellationToken);
    }

    public async Task<Order?> GetInvoiceAsync(Guid customerId, long orderId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Orders
            .AsNoTracking()
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.CustomerId == customerId && x.Id == orderId, cancellationToken);
    }
}