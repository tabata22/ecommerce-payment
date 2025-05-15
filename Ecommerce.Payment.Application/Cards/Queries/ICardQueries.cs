namespace Ecommerce.Payment.Application.Cards.Queries;

public interface ICardQueries
{
    Task<IReadOnlyList<GetCardsDto>> GetActiveCardsAsync(Guid customerId, CancellationToken cancellationToken = default);
}