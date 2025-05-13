using Ecommerce.Payment.Domain.TransactionAggregate;

namespace Ecommerce.Payment.Application.Transactions.Queries;

public interface ITransactionQueries
{
    Task<IReadOnlyList<Transaction>> GetTransactionsAsync(CancellationToken cancellationToken = default);
}