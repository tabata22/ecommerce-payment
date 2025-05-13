using Ecommerce.Payment.Application.PaymentProviders.Requests;
using Ecommerce.Payment.Application.PaymentProviders.Responses;

namespace Ecommerce.Payment.Application.PaymentProviders;

public interface IPaymentClient
{
    Task<OrderResponse> OrderAsync(OrderRequest request, CancellationToken cancellationToken = default);
    
    Task<BaseResponse> SaveCardAsync(Guid orderId, CancellationToken cancellationToken = default);
    
    Task<BaseResponse> PayByCardAsync(PayByCardRequest request, CancellationToken cancellationToken = default);
    
    Task<BaseResponse> RefundAsync(RefundRequest request, CancellationToken cancellationToken = default);

    void ValidateSignature(string signature, string data);
}