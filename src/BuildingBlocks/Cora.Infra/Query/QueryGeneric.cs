using AutoMapper;
using Cora.Infra.Context;
using Core.Domain.Data;
using Core.Domain.Data.Pagination;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Cora.Infra.Query
{
    [ExcludeFromCodeCoverage]
    public abstract class QueryGeneric<TEntity, TResponse>(IContext context, IMapper mapper) : IQueryGeneric<TEntity, TResponse>
        where TEntity : Entity
    {
        protected readonly IContext _context = context;
        protected readonly IMapper _mapper = mapper;

        public virtual async Task<IEnumerable<TResponse>> FindAllAsync(CancellationToken cancellationToken
                                                                      , Expression<Func<TEntity, object>>? orderBy = null
                                                                      , PaginationOptions? paginationOptions = null
                                                                      , string? stringFilter = null)
        {
            var query = _context.GetDbSet<TEntity>().AsNoTracking();

            if (paginationOptions != null)
            {
                // Ordenação
                if (orderBy != null)
                {
                    query = paginationOptions._sort == SortType.Asc
                        ? query.OrderBy(orderBy)
                        : query.OrderByDescending(orderBy);
                }

                // Paginação
                query = query.Skip(paginationOptions.GetOffsetSize()).Take(paginationOptions._size);
            }

            var result = await query.ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<TResponse>>(result);
        }

        public virtual async Task<TResponse> FindByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _context.GetDbSet<TEntity>().AsNoTracking().FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

            return _mapper.Map<TResponse>(result);
        }
    }
}