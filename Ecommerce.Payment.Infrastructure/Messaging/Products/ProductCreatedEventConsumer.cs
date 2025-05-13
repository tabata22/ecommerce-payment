using Ecommerce.Payment.Application.Products.Commands;
using Ecommerce.Payment.Infrastructure.Messaging.Products.Events;
using MassTransit;
using MediatR;

namespace Ecommerce.Payment.Infrastructure.Messaging.Products;

public class ProductCreatedEventConsumer : IConsumer<ProductCreatedEvent>
{
    private readonly IMediator _mediator;

    public ProductCreatedEventConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<ProductCreatedEvent> context)
    {
        var @event = context.Message;
        
        var command = new CreateProductCommand(
            @event.Id,
            @event.Name,
            @event.Price,
            @event.Image);
        
        await _mediator.Send(command, context.CancellationToken);
    }
}