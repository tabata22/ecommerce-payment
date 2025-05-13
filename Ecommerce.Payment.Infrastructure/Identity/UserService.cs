using Ecommerce.Payment.Application.Identity;

namespace Ecommerce.Payment.Infrastructure.Identity;

public class UserService : IUserService
{
    public Guid GetUserId { get; }
}