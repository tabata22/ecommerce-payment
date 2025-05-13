using Ecommerce.Payment.Application.Cards.Commands;
using Ecommerce.Payment.Application.Cards.Queries;
using Ecommerce.Payment.Application.Identity;
using MediatR;

namespace Ecommerce.Payment.Api.Endpoints;

public static class CardEndpoints
{
    public static RouteGroupBuilder RegisterCardEndpoints(this RouteGroupBuilder routeGroup)
    {
        var group = routeGroup.MapGroup("/cards")
            .WithTags("Card");
        
        group.MapGet("/", async (
            IUserService userService,
            ICardQueries queries,
            CancellationToken cancellationToken = default
        ) =>
        {
            var userId = userService.GetUserId;
            var cards = await queries.GetActiveCardsAsync(userId, cancellationToken);
            
            return Results.Ok(cards);
        });
        
        group.MapDelete("/{id:guid}", async (
            IMediator mediator,
            Guid id,
            CancellationToken cancellationToken = default
            ) =>
        {
            var command = new DeleteCardCommand(id);
            
            var result = await mediator.Send(command, cancellationToken);
            
            return result.ToResponse();
        });
        
        return group;
    }
}