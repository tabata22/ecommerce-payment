using Ecommerce.Payment.Domain.OrderAggregate;
using Ecommerce.Payment.Domain.TransactionAggregate.Events;
using MediatR;

namespace Ecommerce.Payment.Application.Orders.EventHandlers;

public class TransactionCompletedDomainEventHandler : INotificationHandler<TransactionCompletedDomainEvent>
{
    private readonly IOrderRepository _orderRepository;

    public TransactionCompletedDomainEventHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task Handle(TransactionCompletedDomainEvent @event, CancellationToken cancellationToken)
    {
        
    }
}