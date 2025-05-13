using Ecommerce.Payment.Application.Identity;
using Ecommerce.Payment.Application.Orders.Queries;

namespace Ecommerce.Payment.Api.Endpoints;

public static class OrderEndpoints
{
    public static RouteGroupBuilder RegisterOrderEndpoints(this RouteGroupBuilder routeGroup)
    {
        var group = routeGroup.MapGroup("/orders")
            .WithTags("Order");

        group.MapGet("/", async (
            IUserService userService,
            IOrderQueries queries,
            CancellationToken cancellationToken = default
            ) =>
        {
            var orders = await queries.GetOrders(userService.GetUserId, cancellationToken);
            
            return Results.Ok(orders); 
        });
        
        group.MapGet("/invoice/{id:long}", async (
            IUserService userService,
            IOrderQueries queries,
            long id,
            CancellationToken cancellationToken = default
        ) =>
        {
            var order = await queries.GetInvoice(userService.GetUserId, id, cancellationToken);
            if (order is null)
            {
                return Results.NotFound();
            }
            
            return Results.Ok(order); 
        });
        
        return group;
    }
}