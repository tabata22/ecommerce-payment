using CSharpFunctionalExtensions;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace Ecommerce.Payment.Api.Endpoints;

public static class EndpointsExtension
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        var mainGroup = app.MapGroup("api/v1")
            .WithOpenApi();

        mainGroup.RegisterCardEndpoints();
        mainGroup.RegisterTransactionEndpoints();
        mainGroup.RegisterOrderEndpoints();
        
        return app;
    }

    public static IResult ToResponse(this Result result)
    {
        if (result.IsSuccess)
        {
            return Results.Ok();
        }
            
        return Results.BadRequest(result.Error);
    }
    
    public static IResult ToResponse<T>(this Result<T> result)
    {
        if (result.IsFailure)
        {
            return Results.BadRequest(result.Error);
        }

        if (result.Value is null)
        {
            return Results.NotFound();
        }
        
        return Results.Ok(result.Value);
    }
}