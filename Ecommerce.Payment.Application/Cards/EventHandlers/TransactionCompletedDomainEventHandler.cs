using Ecommerce.Payment.Application.Cards.Commands;
using Ecommerce.Payment.Domain.TransactionAggregate.Events;
using MediatR;

namespace Ecommerce.Payment.Application.Cards.EventHandlers;

public class TransactionCompletedDomainEventHandler : INotificationHandler<TransactionCompletedDomainEvent>
{
    private readonly IMediator _mediator;

    public TransactionCompletedDomainEventHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(TransactionCompletedDomainEvent @event, CancellationToken cancellationToken)
    {
        var command = new SaveCardCommand(
            @event.ProviderId,
            @event.CustomerId,
            @event.CardNumber,
            @event.CardExpiration,
            @event.CardType);
        
        await _mediator.Send(command, cancellationToken);
    }
}