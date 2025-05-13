using CSharpFunctionalExtensions;
using Ecommerce.Payment.Domain.CardAggregate;
using Ecommerce.Payment.Domain.TransactionAggregate;
using MediatR;

namespace Ecommerce.Payment.Application.Transactions.Commands;

public record FinishTransactionCommand(
    Guid ProviderId,
    TransactionStatus Status,
    string CardNumber,
    DateOnly CardExpiration,
    CardType CardType) 
    : IRequest<Result>;

public class FinishTransactionCommandHandler : IRequestHandler<FinishTransactionCommand, Result>
{
    private readonly ITransactionRepository _transactionRepository;

    public FinishTransactionCommandHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<Result> Handle(FinishTransactionCommand command, CancellationToken cancellationToken)
    {
        var transaction = await _transactionRepository.GetForUpdateAsync(x => x.ProviderId == command.ProviderId, cancellationToken);
        if (transaction is null)
        {
            return Result.Failure("Transaction not found");
        }

        if (transaction.Status != TransactionStatus.Pending)
        {
            return Result.Failure("Transaction is not in pending status");
        }

        if (command.Status == TransactionStatus.Completed)
        {
            transaction.Complete(command.CardNumber, command.CardExpiration, command.CardType);
        }
        else
        {
            transaction.Fail();
        }

        await _transactionRepository.SaveAsync(cancellationToken);
        
        return Result.Success();
    }
}