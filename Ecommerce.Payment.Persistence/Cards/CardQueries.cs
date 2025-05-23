using Ecommerce.Payment.Application.Cards.Queries;
using Ecommerce.Payment.Domain.CardAggregate;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Payment.Persistence.Cards;

public class CardQueries : ICardQueries
{
    private readonly ApplicationDbContext  _dbContext;

    public CardQueries(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<GetCardsDto>> GetActiveCardsAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        var date = DateOnly.FromDateTime(DateTime.UtcNow);
        
        return await _dbContext.Cards
            .AsNoTracking()
            .Where(c => c.CustomerId == customerId)
            .Select(c => new GetCardsDto
            {
                Id = c.Id,
                Number = c.Number,
                Type = c.Type,
                IsExpired = c.ExpirationDate > date
            })
            .ToListAsync(cancellationToken);
    }
}