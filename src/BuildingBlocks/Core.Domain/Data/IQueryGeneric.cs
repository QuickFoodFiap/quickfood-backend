using Core.Domain.Data.Pagination;
using System.Linq.Expressions;

namespace Core.Domain.Data
{
    public interface IQueryGeneric<TEntity, TResponse>
    {
        Task<IEnumerable<TResponse>> FindAllAsync(CancellationToken cancellationToken
                                                 , Expression<Func<TEntity, object>>? orderBy = null
                                                 , PaginationOptions? paginationOptions = null
                                                 , string? stringFilter = null);

        Task<TResponse> FindByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}