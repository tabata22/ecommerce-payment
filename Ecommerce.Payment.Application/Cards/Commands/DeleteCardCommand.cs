using CSharpFunctionalExtensions;
using Ecommerce.Payment.Domain.CardAggregate;
using MediatR;

namespace Ecommerce.Payment.Application.Cards.Commands;

public record DeleteCardCommand(Guid Id) : IRequest<Result>;

public class DeleteCardCommandHandler : IRequestHandler<DeleteCardCommand, Result>
{
    private readonly ICardRepository _cardRepository;

    public DeleteCardCommandHandler(ICardRepository cardRepository)
    {
        _cardRepository = cardRepository;
    }

    public async Task<Result> Handle(DeleteCardCommand command, CancellationToken cancellationToken)
    {
        await _cardRepository.DeleteAsync(x => x.Id == command.Id, cancellationToken);
        
        await _cardRepository.SaveAsync(cancellationToken);
        
        return Result.Success();
    }
}