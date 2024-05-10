using Core.Domain.Entities;
using System.Linq.Expressions;

namespace Core.Domain.Data
{
    public interface IRepositoryGeneric<TEntity> : IRepository where TEntity : Entity
    {
        Task<TEntity?> FindByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<List<TEntity>> FindAllAsync(CancellationToken cancellationToken);

        Task InsertAsync(TEntity entity, CancellationToken cancellationToken);

        Task InsertRangeAsync(List<TEntity> entityList, CancellationToken cancellationToken);

        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);

        Task UpdateRangeAsync(List<TEntity> entityList, CancellationToken cancellationToken);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken);

        Task DeleteRangeAsync(List<TEntity> entityList, CancellationToken cancellationToken);

        IQueryable<TEntity> Query();

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    }
}