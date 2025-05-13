using System.Linq.Expressions;
using Ecommerce.Payment.Domain;

namespace Ecommerce.Payment.Persistence;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    public Task<TEntity?> GetForUpdateAsync(Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity?> GetForUpdateAsync(long id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity?> GetAsync(long id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task ExistsAsync(Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}