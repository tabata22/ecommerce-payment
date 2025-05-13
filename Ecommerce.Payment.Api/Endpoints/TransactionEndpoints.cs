using Ecommerce.Payment.Api.Requests;
using Ecommerce.Payment.Application.PaymentProviders;
using Ecommerce.Payment.Application.Transactions.Commands;
using Ecommerce.Payment.Application.Transactions.Queries;
using Ecommerce.Payment.Domain.TransactionAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Payment.Api.Endpoints;

public static class TransactionEndpoints
{
    public static RouteGroupBuilder RegisterTransactionEndpoints(this RouteGroupBuilder routeGroup)
    {
        var group = routeGroup.MapGroup("/transactions")
            .WithTags("Transaction");
        
        group.MapGet("/", async (
            ITransactionQueries queries,
            CancellationToken cancellationToken = default
        ) =>
        {
            var transactions = await queries.GetTransactionsAsync(cancellationToken);
            
            return Results.Ok(transactions);
        });
        
        group.MapPost("/request-payment", async (
            IMediator mediator,
            RequestPaymentCommand command,
            CancellationToken cancellationToken = default
        ) =>
        {
            var result = await mediator.Send(command, cancellationToken);
            
            return result.ToResponse();
        });
        
        group.MapPost("/finish", async (
            HttpContext httpContext,
            IPaymentClient paymentClient,
            IMediator mediator,
            FinishTransactionRequest request,
            [FromHeader(Name = "Callback-Signature")] string signature,
            CancellationToken cancellationToken = default
        ) =>
        {
            using var reader = new StreamReader(httpContext.Request.Body);
            var bodyData = await reader.ReadToEndAsync(cancellationToken);
                
            paymentClient.ValidateSignature(signature, bodyData);
            
            var command = new FinishTransactionCommand(
                Guid.Parse(request.Body.OrderId),
                request.Body.PaymentDetails.Status,
                request.Body.PaymentDetails.PayerIdentifier,
                request.Body.PaymentDetails.CardExpiryDate,
                request.Body.PaymentDetails.CardType);
            
            var result = await mediator.Send(command, cancellationToken);
            
            return result.ToResponse();
        });
        
        group.MapPost("/refund/{id:long}", async (
            IMediator mediator,
            long id,
            CancellationToken cancellationToken = default
        ) =>
        {
            var command = new RefundTransactionCommand(id);
            var result = await mediator.Send(command, cancellationToken);
            
            return result.ToResponse();
        });
        
        return group;
    }
}