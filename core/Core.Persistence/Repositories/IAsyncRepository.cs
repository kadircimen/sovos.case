using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Core.Persistence.Repositories;
public interface IAsyncRepository<T> : IQuery<T> where T : BaseEntity
{
    Task<T?> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
    Task<IPaginate<T>> GetListAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, int index = 0, int size = 10, bool enableTracking = true, CancellationToken cancellationToken = default);
    Task<T> AddAsync(T entity);
    Task<IEnumerable<T>> UpdateRangeAsync(IEnumerable<T> entity);
    Task<T> AddWithRelationAsync(T entity, Action<T> relatedEntityAction);
}
