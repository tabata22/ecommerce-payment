using CSharpFunctionalExtensions;
using Ecommerce.Payment.Domain.ProductAggregate;
using MediatR;

namespace Ecommerce.Payment.Application.Products.Commands;

public record CreateProductCommand(long Id, string Name, decimal Price, string Image) : IRequest<Result>;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result>
{
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = new Product(command.Id, command.Name, command.Price, command.Image);

        await _productRepository.AddAsync(product, cancellationToken);
        await _productRepository.SaveAsync(cancellationToken);
        
        return Result.Success();
    }
}