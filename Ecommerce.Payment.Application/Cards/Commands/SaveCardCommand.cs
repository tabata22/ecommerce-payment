using CSharpFunctionalExtensions;
using Ecommerce.Payment.Domain.CardAggregate;
using MediatR;

namespace Ecommerce.Payment.Application.Cards.Commands;

public record SaveCardCommand(
    Guid Id, 
    Guid CustomerId, 
    string CardNumber,
    DateOnly ExpirationDate,
    CardType Type) : IRequest<Result>;
    
public class SaveCardCommandHandler : IRequestHandler<SaveCardCommand, Result>
{
    private readonly ICardRepository _cardRepository;

    public SaveCardCommandHandler(ICardRepository cardRepository)
    {
        _cardRepository = cardRepository;
    }

    public async Task<Result> Handle(SaveCardCommand command, CancellationToken cancellationToken)
    {
        var card = new Card(
            command.Id, 
            command.CustomerId, 
            command.CardNumber, 
            command.ExpirationDate, 
            command.Type);
        
        await _cardRepository.AddAsync(card, cancellationToken);
        await _cardRepository.SaveAsync(cancellationToken);
        
        return Result.Success();
    }
}