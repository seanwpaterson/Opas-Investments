using System.Linq.Expressions;

namespace Opas.Core.Data.Models;

public interface IBaseRepository<TEntity> where TEntity : class
{
    string EntityName();

    string GetName(TEntity entity);

    IQueryable<TEntity> Query();

    Task<TEntity?> GetAsync(int id, CancellationToken cancellationToken = default);

    TEntity? Get(int id);

    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

    IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> expression);

    Task<int> CountAsync(CancellationToken cancellationToken = default);

    int Count();

    Task<int> CountWhereAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);

    int CountWhere(Expression<Func<TEntity, bool>> expression);

    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    TEntity Add(TEntity entity);

    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    void Update(TEntity entity);

    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

    void Delete(TEntity entity);

    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    void AddRange(IEnumerable<TEntity> entities);

    Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    void UpdateRange(IEnumerable<TEntity> entities);

    Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    void DeleteRange(IEnumerable<TEntity> entities);
}
