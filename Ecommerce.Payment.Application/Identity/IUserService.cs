namespace Ecommerce.Payment.Application.Identity;

public interface IUserService
{
    Guid GetUserId { get; }
}