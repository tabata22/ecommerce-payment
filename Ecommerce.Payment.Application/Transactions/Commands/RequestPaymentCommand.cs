using CSharpFunctionalExtensions;
using Ecommerce.Payment.Application.Identity;
using Ecommerce.Payment.Application.PaymentProviders;
using Ecommerce.Payment.Application.PaymentProviders.Requests;
using Ecommerce.Payment.Domain.OrderAggregate;
using Ecommerce.Payment.Domain.TransactionAggregate;
using MediatR;

namespace Ecommerce.Payment.Application.Transactions.Commands;

public record RequestPaymentCommand(
    long OrderId, 
    PaymentMethod PaymentMethod,
    bool SaveCard) 
    : IRequest<Result<string>>;

public class RequestPaymentCommandHandler : IRequestHandler<RequestPaymentCommand, Result<string>>
{
    private readonly ITransactionRepository _repository;
    private readonly IOrderRepository _orderRepository;
    private readonly IPaymentClient  _paymentClient;
    private readonly IUserService _userService;
    private readonly BogPaymentSettings _paymentSettings;

    public RequestPaymentCommandHandler(
        ITransactionRepository repository,
        IPaymentClient paymentClient, 
        IOrderRepository orderRepository, 
        IUserService userService,
        BogPaymentSettings paymentSettings)
    {
        _repository = repository;
        _paymentClient = paymentClient;
        _orderRepository = orderRepository;
        _userService = userService;
        _paymentSettings = paymentSettings;
    }

    public async Task<Result<string>> Handle(RequestPaymentCommand command, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetAsync(command.OrderId, cancellationToken);
        if (order is null)
        {
            return Result.Failure<string>("Order not found");
        }
        
        var request = new OrderRequest
        {
            CallbackUrl = _paymentSettings.CallbackUrl,
            PurchaseUnit = new PurchaseUnit
            {
                Currency = CurrencyCodes.GEL,
                TotalAmount = order.TotalPrice,
                Basket = order.Items.Select(x => new Basket
                {
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice,
                    ProductId = x.ProductId.ToString()
                }).ToList()
            },
            IdempotencyKey = Guid.NewGuid()
        };
        
        var orderResponse = await _paymentClient.OrderAsync(request, cancellationToken);
        if (!orderResponse.IsSuccess)
        {
            return Result.Failure<string>(orderResponse.ErrorMessage);
        }
        
        var transaction = new Transaction(
            order.Id, 
            _userService.GetUserId,
            orderResponse.Id, 
            command.PaymentMethod,
            order.TotalPrice);
        
        await _repository.AddAsync(transaction, cancellationToken);
        await _repository.SaveAsync(cancellationToken);
        
        return Result.Success(orderResponse.Links.Redirect.Href);
    }
}