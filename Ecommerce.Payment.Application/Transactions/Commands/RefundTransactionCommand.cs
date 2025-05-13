using CSharpFunctionalExtensions;
using Ecommerce.Payment.Application.PaymentProviders;
using Ecommerce.Payment.Application.PaymentProviders.Requests;
using Ecommerce.Payment.Domain.TransactionAggregate;
using MediatR;

namespace Ecommerce.Payment.Application.Transactions.Commands;

public record RefundTransactionCommand(long Id) : IRequest<Result>;

public class RefundTransactionCommandHandler : IRequestHandler<RefundTransactionCommand, Result>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IPaymentClient  _paymentClient;

    public RefundTransactionCommandHandler(
        ITransactionRepository transactionRepository, 
        IPaymentClient paymentClient)
    {
        _transactionRepository = transactionRepository;
        _paymentClient = paymentClient;
    }

    public async Task<Result> Handle(RefundTransactionCommand command, CancellationToken cancellationToken)
    {
        var transaction = await _transactionRepository.GetForUpdateAsync(command.Id, cancellationToken);
        if (transaction is null)
        {
            return Result.Failure("Transaction not found");
        }

        var refundRequest = new RefundRequest
        {
            IdempotencyKey = Guid.NewGuid(),
            OrderId = transaction.ProviderId
        };
        
        var response = await _paymentClient.RefundAsync(refundRequest, cancellationToken);
        if (!response.IsSuccess)
        {
            return Result.Failure(response.ErrorMessage);
        }
        
        transaction.Refund();
        await _transactionRepository.SaveAsync(cancellationToken);
        
        return Result.Success();
    }
}