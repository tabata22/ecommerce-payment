using Ecommerce.Payment.Domain.CardAggregate;

namespace Ecommerce.Payment.Application.Cards.Queries;

public interface ICardQueries
{
    Task<IReadOnlyList<Card>> GetActiveCardsAsync(Guid customerId, CancellationToken cancellationToken = default);
}