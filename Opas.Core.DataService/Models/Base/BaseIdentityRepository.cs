using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Linq.Expressions;

namespace Opas.Core.DataService.Models.Base;

public abstract class BaseIdentityRepository<TEntity, TUser> : IBaseIdentityRepository<TEntity, TUser> where TEntity : class where TUser : IdentityUser
{
    protected readonly BaseIdentityRepositoryContext<TUser> _repositoryContext;

    protected BaseIdentityRepository(BaseIdentityRepositoryContext<TUser> repositoryContext)
    {
        _repositoryContext = repositoryContext;
    }

    public DatabaseFacade Database => _repositoryContext.Database;

    public abstract string GetName(TEntity entity);

    public virtual string EntityName()
    {
        return typeof(TEntity).Name;
    }

    public virtual IQueryable<TEntity> Query()
    {
        try
        {
            return _repositoryContext.Set<TEntity>().AsNoTracking();
        }
        catch (Exception ex)
        {
            var innerException = ex.InnerException is null ? "Null InnerException" : ex.InnerException.Message;
            throw new Exception($"Couldn't retrieve entities: {ex.Message}\r\n InnerException: {innerException}");
        }
    }

    public virtual async Task<TEntity?> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
        {
            throw new ArgumentException($"{nameof(GetAsync)} id must not be less than or equal to 0", nameof(id));
        }

        try
        {
            return await _repositoryContext.Set<TEntity>().FindAsync(new object?[] { id }, cancellationToken: cancellationToken);
        }
        catch (Exception ex)
        {
            var innerException = ex.InnerException is null ? "Null InnerException" : ex.InnerException.Message;
            throw new Exception($"{id} could not be found: {ex.Message}\r\n InnerException: {innerException}");
        }
    }

    public virtual TEntity? Get(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException($"{nameof(Get)} id must not be less than or equal to 0", nameof(id));
        }

        try
        {
            return _repositoryContext.Set<TEntity>().Find(id);
        }
        catch (Exception ex)
        {
            var innerException = ex.InnerException is null ? "Null InnerException" : ex.InnerException.Message;
            throw new Exception($"{id} could not be found: {ex.Message}\r\n InnerException: {innerException}");
        }
    }

    public virtual async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
    {
        return await _repositoryContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(expression, cancellationToken);
    }

    public virtual TEntity? FirstOrDefault(Expression<Func<TEntity, bool>> expression)
    {
        return _repositoryContext.Set<TEntity>().AsNoTracking().FirstOrDefault(expression);
    }

    public virtual IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> expression)
    {
        return _repositoryContext.Set<TEntity>().Where(expression).AsNoTracking();
    }

    public virtual async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return await _repositoryContext.Set<TEntity>().AsNoTracking().CountAsync(cancellationToken);
    }

    public virtual int Count()
    {
        return _repositoryContext.Set<TEntity>().AsNoTracking().Count();
    }

    public virtual async Task<int> CountWhereAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
    {
        return await _repositoryContext.Set<TEntity>().AsNoTracking().CountAsync(expression, cancellationToken);
    }

    public virtual int CountWhere(Expression<Func<TEntity, bool>> expression)
    {
        return _repositoryContext.Set<TEntity>().AsNoTracking().Count(expression);
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        try
        {
            _ = await _repositoryContext.AddAsync(entity, cancellationToken);
            _ = await _repositoryContext.SaveChangesAsync(cancellationToken);

            return entity;
        }
        catch (Exception ex)
        {
            var innerException = ex.InnerException is null ? "Null InnerException" : ex.InnerException.Message;
            throw new Exception($"{entity.GetType().Name} could not be saved: {ex.Message}\r\n InnerException: {innerException}");
        }
    }

    public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        if (!entities.Any())
        {
            throw new ArgumentNullException(nameof(entities), $"{nameof(AddRangeAsync)} entities contains no values");
        }

        try
        {
            await _repositoryContext.AddRangeAsync(entities, cancellationToken);
            _ = await _repositoryContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            var innerException = ex.InnerException is null ? "Null InnerException" : ex.InnerException.Message;
            throw new Exception($"Entities could not be saved: {ex.Message}\r\n InnerException: {innerException}");
        }
    }

    public virtual TEntity Add(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        try
        {
            _ = _repositoryContext.Add(entity);
            _ = _repositoryContext.SaveChanges();

            return entity;
        }
        catch (Exception ex)
        {
            var innerException = ex.InnerException is null ? "Null InnerException" : ex.InnerException.Message;
            throw new Exception($"{entity.GetType().Name} could not be saved: {ex.Message}\r\n InnerException: {innerException}");
        }
    }

    public virtual void AddRange(IEnumerable<TEntity> entities)
    {
        if (!entities.Any())
        {
            throw new ArgumentNullException(nameof(entities), $"{nameof(AddRange)} entities contains no values");
        }

        try
        {
            _repositoryContext.AddRange(entities);
            _ = _repositoryContext.SaveChanges();
        }
        catch (Exception ex)
        {
            var innerException = ex.InnerException is null ? "Null InnerException" : ex.InnerException.Message;
            throw new Exception($"Entities could not be saved: {ex.Message}\r\n InnerException: {innerException}");
        }
    }

    public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        try
        {
            _ = _repositoryContext.Update(entity);
            _ = await _repositoryContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            var innerException = ex.InnerException is null ? "Null InnerException" : ex.InnerException.Message;
            throw new Exception($"{entity.GetType().Name} could not be updated: {ex.Message}\r\n InnerException: {innerException}");
        }
    }

    public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        if (!entities.Any())
        {
            throw new ArgumentNullException(nameof(entities), $"{nameof(UpdateRangeAsync)} entities contains no values");
        }

        try
        {
            _repositoryContext.UpdateRange(entities);
            _ = await _repositoryContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            var innerException = ex.InnerException is null ? "Null InnerException" : ex.InnerException.Message;
            throw new Exception($"Entities could not be saved: {ex.Message}\r\n InnerException: {innerException}");
        }
    }

    public virtual void Update(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        try
        {
            _ = _repositoryContext.Update(entity);
            _ = _repositoryContext.SaveChanges();
        }
        catch (Exception ex)
        {
            var innerException = ex.InnerException is null ? "Null InnerException" : ex.InnerException.Message;
            throw new Exception($"{entity.GetType().Name} could not be updated: {ex.Message}\r\n InnerException: {innerException}");
        }
    }

    public virtual void UpdateRange(IEnumerable<TEntity> entities)
    {
        if (!entities.Any())
        {
            throw new ArgumentNullException(nameof(entities), $"{nameof(UpdateRangeAsync)} entities contains no values");
        }

        try
        {
            _repositoryContext.UpdateRange(entities);
            _ = _repositoryContext.SaveChanges();
        }
        catch (Exception ex)
        {
            var innerException = ex.InnerException is null ? "Null InnerException" : ex.InnerException.Message;
            throw new Exception($"Entities could not be saved: {ex.Message}\r\n InnerException: {innerException}");
        }
    }

    public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        try
        {
            _ = _repositoryContext.Remove(entity);
            _ = await _repositoryContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            var innerException = ex.InnerException is null ? "Null InnerException" : ex.InnerException.Message;
            throw new Exception($"{entity.GetType().Name} could not be deleted: {ex.Message}\r\n InnerException: {innerException}");
        }
    }

    public virtual async Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        if (!entities.Any())
        {
            throw new ArgumentNullException(nameof(entities), $"{nameof(DeleteRangeAsync)} entities contains no values");
        }

        try
        {
            _repositoryContext.RemoveRange(entities);
            _ = await _repositoryContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            var innerException = ex.InnerException is null ? "Null InnerException" : ex.InnerException.Message;
            throw new Exception($"Entities could not be deleted: {ex.Message}\r\n InnerException: {innerException}");
        }
    }

    public virtual void Delete(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        try
        {
            _ = _repositoryContext.Remove(entity);
            _ = _repositoryContext.SaveChanges();
        }
        catch (Exception ex)
        {
            var innerException = ex.InnerException is null ? "Null InnerException" : ex.InnerException.Message;

            throw new Exception($"{entity.GetType().Name} could not be deleted: {ex.Message}\r\n InnerException: {innerException}");
        }
    }

    public virtual void DeleteRange(IEnumerable<TEntity> entities)
    {
        if (!entities.Any())
        {
            throw new ArgumentNullException(nameof(entities), $"{nameof(DeleteRange)} entities contains no values");
        }

        try
        {
            _repositoryContext.RemoveRange(entities);
            _ = _repositoryContext.SaveChanges();
        }
        catch (Exception ex)
        {
            var innerException = ex.InnerException is null ? "Null InnerException" : ex.InnerException.Message;
            throw new Exception($"Entities could not be deleted: {ex.Message}\r\n InnerException: {innerException}");
        }
    }
}
