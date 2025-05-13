using Ecommerce.Payment.Application.Transactions.Queries;
using Ecommerce.Payment.Domain.TransactionAggregate;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Payment.Persistence.Transactions;

public class TransactionQueries : ITransactionQueries
{
    private readonly ApplicationDbContext _dbContext;

    public TransactionQueries(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<Transaction>> GetTransactionsAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Transactions
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}