using CSharpFunctionalExtensions;
using MediatR;

namespace Ecommerce.Payment.Application.Orders.Commands;

public record CreateOrderCommand(
    string? AddressFullName,
    string AddressPhone,
    string AddressRegion,
    string AddressCity,
    string AddressStreet,
    string AddressZipCode) : IRequest<Result>;
    
public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result>
{
    public async Task<Result> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        return Result.Success();
    }
}