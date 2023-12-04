using Core.Persistence.Paging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
namespace Core.Persistence.Repositories;
public class EfRepositoryBase<TEntity, TContext> : IAsyncRepository<TEntity>
    where TEntity : BaseEntity
    where TContext : DbContext
{
    protected TContext Context { get; }
    public EfRepositoryBase(TContext context)
    { Context = context; }
    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        var query = Context.Set<TEntity>().AsQueryable();
        if (include != null) query = include(query);
        return await query.FirstOrDefaultAsync(predicate);
    }
    public async Task<IPaginate<TEntity>> GetListAsync(
          Expression<Func<TEntity, bool>>? predicate = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
          Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, int index = 0, int size = 10, bool enableTracking = true, CancellationToken cancellationToken = default
      )
    {
        IQueryable<TEntity> queryable = Query();
        if (!enableTracking)
            queryable = queryable.AsNoTracking();
        if (include != null)
            queryable = include(queryable);
        if (predicate != null)
            queryable = queryable.Where(predicate);
        if (orderBy != null)
            return await orderBy(queryable).ToPaginateAsync(index, size, from: 0, cancellationToken);
        return await queryable.ToPaginateAsync(index, size, from: 0, cancellationToken);
    }
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        Context.Entry(entity).State = EntityState.Added;
        await Context.SaveChangesAsync();
        return entity;
    }
    public async Task<TEntity> AddWithRelationAsync(TEntity entity, Action<TEntity> relatedEntityAction)
    {
        Context.Entry(entity).State = EntityState.Added;
        relatedEntityAction?.Invoke(entity);
        await Context.SaveChangesAsync();
        return entity;
    }
    public async Task<IEnumerable<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> entity)
    {
        foreach (var e in entity)
        {
            Context.Entry(e).State = EntityState.Modified;
        }
        await Context.SaveChangesAsync();
        return entity;
    }
    public IQueryable<TEntity> Query()
    {
        return Context.Set<TEntity>();
    }
}
