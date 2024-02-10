using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Opas.Core.Data.Models;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly BaseRepositoryContext _repositoryContext;

    public DatabaseFacade Database => _repositoryContext.Database;

    public BaseRepository(BaseRepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
    }

    public abstract string GetName(TEntity entity);

    public string EntityName()
    {
        return typeof(TEntity).Name;
    }

    public IQueryable<TEntity> Query()
    {
        try
        {
            return _repositoryContext.Set<TEntity>().AsNoTracking();
        }
        catch (Exception ex)
        {
            var text = ex.InnerException == null ? "Null InnerException" : ex.InnerException!.Message;
            throw new Exception("Couldn't retrieve entities: " + ex.Message + "\r\n InnerException: " + text);
        }
    }

    public virtual async Task<TEntity?> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
        {
            throw new ArgumentException("GetAsync id must not be less than or equal to 0", "id");
        }

        try
        {
            return await _repositoryContext.Set<TEntity>().FindAsync(new object[1] { id }, cancellationToken);
        }
        catch (Exception ex)
        {
            var value = ex.InnerException == null ? "Null InnerException" : ex.InnerException!.Message;
            new DefaultInterpolatedStringHandler(40, 3).AppendFormatted(id);
            new DefaultInterpolatedStringHandler(40, 3).AppendLiteral(" could not be found: ");
            new DefaultInterpolatedStringHandler(40, 3).AppendFormatted(ex.Message);
            new DefaultInterpolatedStringHandler(40, 3).AppendLiteral("\r\n InnerException: ");
            new DefaultInterpolatedStringHandler(40, 3).AppendFormatted(value);
            throw new Exception(new DefaultInterpolatedStringHandler(40, 3).ToStringAndClear());
        }
    }

    public virtual TEntity? Get(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Get id must not be less than or equal to 0", "id");
        }

        try
        {
            return _repositoryContext.Set<TEntity>().Find(id);
        }
        catch (Exception ex)
        {
            var value = ex.InnerException == null ? "Null InnerException" : ex.InnerException!.Message;
            new DefaultInterpolatedStringHandler(40, 3).AppendFormatted(id);
            new DefaultInterpolatedStringHandler(40, 3).AppendLiteral(" could not be found: ");
            new DefaultInterpolatedStringHandler(40, 3).AppendFormatted(ex.Message);
            new DefaultInterpolatedStringHandler(40, 3).AppendLiteral("\r\n InnerException: ");
            new DefaultInterpolatedStringHandler(40, 3).AppendFormatted(value);
            throw new Exception(new DefaultInterpolatedStringHandler(40, 3).ToStringAndClear());
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
        ArgumentNullException.ThrowIfNull(entity, "entity");

        try
        {
            _ = await _repositoryContext.AddAsync(entity, cancellationToken);
            _ = await _repositoryContext.SaveChangesAsync(cancellationToken);
            return entity;
        }
        catch (Exception ex)
        {
            var value = ex.InnerException == null ? "Null InnerException" : ex.InnerException!.Message;
            new DefaultInterpolatedStringHandler(40, 3).AppendFormatted(entity.GetType().Name);
            new DefaultInterpolatedStringHandler(40, 3).AppendLiteral(" could not be saved: ");
            new DefaultInterpolatedStringHandler(40, 3).AppendFormatted(ex.Message);
            new DefaultInterpolatedStringHandler(40, 3).AppendLiteral("\r\n InnerException: ");
            new DefaultInterpolatedStringHandler(40, 3).AppendFormatted(value);
            throw new Exception(new DefaultInterpolatedStringHandler(40, 3).ToStringAndClear());
        }
    }

    public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        if (!entities.Any())
        {
            throw new ArgumentNullException("entities", "AddRangeAsync entities contains no values");
        }

        try
        {
            await _repositoryContext.AddRangeAsync(entities, cancellationToken);
            _ = await _repositoryContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            var text = ex.InnerException == null ? "Null InnerException" : ex.InnerException!.Message;
            throw new Exception("Entities could not be saved: " + ex.Message + "\r\n InnerException: " + text);
        }
    }

    public virtual TEntity Add(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity, "entity");

        try
        {
            _ = _repositoryContext.Add(entity);
            _ = _repositoryContext.SaveChanges();
            return entity;
        }
        catch (Exception ex)
        {
            var value = ex.InnerException == null ? "Null InnerException" : ex.InnerException!.Message;
            new DefaultInterpolatedStringHandler(40, 3).AppendFormatted(entity.GetType().Name);
            new DefaultInterpolatedStringHandler(40, 3).AppendLiteral(" could not be saved: ");
            new DefaultInterpolatedStringHandler(40, 3).AppendFormatted(ex.Message);
            new DefaultInterpolatedStringHandler(40, 3).AppendLiteral("\r\n InnerException: ");
            new DefaultInterpolatedStringHandler(40, 3).AppendFormatted(value);
            throw new Exception(new DefaultInterpolatedStringHandler(40, 3).ToStringAndClear());
        }
    }

    public virtual void AddRange(IEnumerable<TEntity> entities)
    {
        if (!entities.Any())
        {
            throw new ArgumentNullException("entities", "AddRange entities contains no values");
        }

        try
        {
            _repositoryContext.AddRange(entities);
            _ = _repositoryContext.SaveChanges();
        }
        catch (Exception ex)
        {
            var text = ex.InnerException == null ? "Null InnerException" : ex.InnerException!.Message;
            throw new Exception("Entities could not be saved: " + ex.Message + "\r\n InnerException: " + text);
        }
    }

    public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity, "entity");

        try
        {
            _ = _repositoryContext.Update(entity);
            _ = await _repositoryContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            var value = ex.InnerException == null ? "Null InnerException" : ex.InnerException!.Message;
            new DefaultInterpolatedStringHandler(42, 3).AppendFormatted(entity.GetType().Name);
            new DefaultInterpolatedStringHandler(42, 3).AppendLiteral(" could not be updated: ");
            new DefaultInterpolatedStringHandler(42, 3).AppendFormatted(ex.Message);
            new DefaultInterpolatedStringHandler(42, 3).AppendLiteral("\r\n InnerException: ");
            new DefaultInterpolatedStringHandler(42, 3).AppendFormatted(value);
            throw new Exception(new DefaultInterpolatedStringHandler(42, 3).ToStringAndClear());
        }
    }

    public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        if (!entities.Any())
        {
            throw new ArgumentNullException("entities", "UpdateRangeAsync entities contains no values");
        }

        try
        {
            _repositoryContext.UpdateRange(entities);
            _ = await _repositoryContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            var text = ex.InnerException == null ? "Null InnerException" : ex.InnerException!.Message;
            throw new Exception("Entities could not be saved: " + ex.Message + "\r\n InnerException: " + text);
        }
    }

    public virtual void Update(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity, "entity");

        try
        {
            _ = _repositoryContext.Update(entity);
            _ = _repositoryContext.SaveChanges();
        }
        catch (Exception ex)
        {
            var value = ex.InnerException == null ? "Null InnerException" : ex.InnerException!.Message;
            new DefaultInterpolatedStringHandler(42, 3).AppendFormatted(entity.GetType().Name);
            new DefaultInterpolatedStringHandler(42, 3).AppendLiteral(" could not be updated: ");
            new DefaultInterpolatedStringHandler(42, 3).AppendFormatted(ex.Message);
            new DefaultInterpolatedStringHandler(42, 3).AppendLiteral("\r\n InnerException: ");
            new DefaultInterpolatedStringHandler(42, 3).AppendFormatted(value);
            throw new Exception(new DefaultInterpolatedStringHandler(42, 3).ToStringAndClear());
        }
    }

    public virtual void UpdateRange(IEnumerable<TEntity> entities)
    {
        if (!entities.Any())
        {
            throw new ArgumentNullException("entities", "UpdateRangeAsync entities contains no values");
        }

        try
        {
            _repositoryContext.UpdateRange(entities);
            _ = _repositoryContext.SaveChanges();
        }
        catch (Exception ex)
        {
            var text = ex.InnerException == null ? "Null InnerException" : ex.InnerException!.Message;
            throw new Exception("Entities could not be saved: " + ex.Message + "\r\n InnerException: " + text);
        }
    }

    public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity, "entity");

        try
        {
            _ = _repositoryContext.Remove(entity);
            _ = await _repositoryContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            var value = ex.InnerException == null ? "Null InnerException" : ex.InnerException!.Message;
            new DefaultInterpolatedStringHandler(42, 3).AppendFormatted(entity.GetType().Name);
            new DefaultInterpolatedStringHandler(42, 3).AppendLiteral(" could not be deleted: ");
            new DefaultInterpolatedStringHandler(42, 3).AppendFormatted(ex.Message);
            new DefaultInterpolatedStringHandler(42, 3).AppendLiteral("\r\n InnerException: ");
            new DefaultInterpolatedStringHandler(42, 3).AppendFormatted(value);
            throw new Exception(new DefaultInterpolatedStringHandler(42, 3).ToStringAndClear());
        }
    }

    public virtual async Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        if (!entities.Any())
        {
            throw new ArgumentNullException("entities", "DeleteRangeAsync entities contains no values");
        }

        try
        {
            _repositoryContext.RemoveRange(entities);
            _ = await _repositoryContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            var text = ex.InnerException == null ? "Null InnerException" : ex.InnerException!.Message;
            throw new Exception("Entities could not be deleted: " + ex.Message + "\r\n InnerException: " + text);
        }
    }

    public virtual void Delete(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity, "entity");
        try
        {
            _ = _repositoryContext.Remove(entity);
            _ = _repositoryContext.SaveChanges();
        }
        catch (Exception ex)
        {
            var value = ex.InnerException == null ? "Null InnerException" : ex.InnerException!.Message;
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new(42, 3);
            defaultInterpolatedStringHandler.AppendFormatted(entity.GetType().Name);
            defaultInterpolatedStringHandler.AppendLiteral(" could not be deleted: ");
            defaultInterpolatedStringHandler.AppendFormatted(ex.Message);
            defaultInterpolatedStringHandler.AppendLiteral("\r\n InnerException: ");
            defaultInterpolatedStringHandler.AppendFormatted(value);
            throw new Exception(defaultInterpolatedStringHandler.ToStringAndClear());
        }
    }

    public virtual void DeleteRange(IEnumerable<TEntity> entities)
    {
        if (!entities.Any())
        {
            throw new ArgumentNullException("entities", "DeleteRange entities contains no values");
        }

        try
        {
            _repositoryContext.RemoveRange(entities);
            _ = _repositoryContext.SaveChanges();
        }
        catch (Exception ex)
        {
            var text = ex.InnerException == null ? "Null InnerException" : ex.InnerException!.Message;
            throw new Exception("Entities could not be deleted: " + ex.Message + "\r\n InnerException: " + text);
        }
    }
}