using System.Linq.Expressions;

namespace Ecommerce.Payment.Domain;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity?> GetForUpdateAsync(Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken = default);
    
    Task<TEntity?> GetForUpdateAsync(long id, CancellationToken cancellationToken = default);
    
    Task<TEntity?> GetAsync(long id, CancellationToken cancellationToken = default);
    
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    
    Task ExistsAsync(Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken = default);
    
    Task DeleteAsync(Expression<Func<TEntity, bool>> where, CancellationToken cancellationToken = default);
    
    Task SaveAsync(CancellationToken cancellationToken = default);
}