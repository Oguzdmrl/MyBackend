using Core.Results;
using DataAccess.Context;
using DataAccess.UnitOfWorks;
using Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace DataAccess.Repository;

public class EfRepositoryBase<TEntity, TKey, TContext> : IAsyncRepository<TEntity, TKey>
 where TEntity : Entity<TKey>
 where TContext : DbContext
{
    protected readonly TContext Context;
    private readonly IUnitOfWork _uow;
    private ResponseDataResult<TEntity> _response;
    public EfRepositoryBase(TContext context, IUnitOfWork uow)
    {
        Context = context;
        _response = new(false, "İçerik boş");
        _uow = uow;
    }

    #region Async Repository

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        entity.CreatedDate = DateTime.UtcNow;
        await Context.AddAsync(entity);
        await _uow.SaveChangesAsync();
        return entity;
    }

    public async Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities)
    {
        foreach (var entity in entities)
            entity.CreatedDate = DateTime.UtcNow;
        await Context.AddAsync(entities);
        await _uow.SaveChangesAsync();
        return entities;
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> queryable = Query();
        if (!enableTracking)
            queryable = queryable.AsNoTracking();
        if (withDeleted)
            queryable = queryable.IgnoreQueryFilters();
        if (predicate != null)
            queryable.Where(predicate);
        return await queryable.AnyAsync(cancellationToken);
    }

    public async Task<TEntity> DeleteAsync(TEntity entity)
    {
        entity.DeletedDate = DateTime.UtcNow;
        entity.WithDeleted = true;
        Context.Update(entity);
        await _uow.SaveChangesAsync();
        return entity;
    }

    public async Task<ICollection<TEntity>> DeleteRangeAsync(ICollection<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            entity.DeletedDate = DateTime.UtcNow;
            entity.WithDeleted = true;
        }
        Context.UpdateRange(entities);
        await _uow.SaveChangesAsync();
        return entities;

    }

    public async Task<ResponseDataResult<TEntity>> GetAsnyc(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> queryable = Query();
        if (!enableTracking)
            queryable = queryable.AsNoTracking();
        if (include != null)
            queryable = include(queryable);
        if (withDeleted)
            queryable = queryable.IgnoreQueryFilters();

        return await Task.FromResult(new ResponseDataResult<TEntity>()
        {
            ResponseModel = await queryable.FirstOrDefaultAsync(predicate, cancellationToken),
            Status = true,
            Message = "Listeleme İşlemi Başarılı."
        });

    }

    public async Task<ResponseDataResult<TEntity?>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> queryable = Query();
        if (!enableTracking)
            queryable = queryable.AsNoTracking();
        if (include != null)
            queryable = include(queryable);
        if (withDeleted)
            queryable = queryable.IgnoreQueryFilters();
        if (predicate != null)
            queryable = queryable.Where(predicate);

        IEnumerable<TEntity> Model = await queryable.ToListAsync(cancellationToken);
        if (Model.Count() == 0) return await Task.FromResult(_response);

        return await Task.FromResult(new ResponseDataResult<TEntity>()
        {
            ListResponseModel = Model,
            Status = true,
            Message = "Listeleme İşlemi Başarılı.",
            ModelCount = Model.ToList().Count,
        });

    }
    public IQueryable<TEntity> Query() => Context.Set<TEntity>();

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        entity.UpdatedDate = DateTime.UtcNow;
        Context.Update(entity);
        await _uow.SaveChangesAsync();
        return entity;
    }

    public async Task<ICollection<TEntity>> UpdateRangeAsync(ICollection<TEntity> entities)
    {
        foreach (TEntity entity in entities)
            entity.UpdatedDate = DateTime.UtcNow;
        Context.Update(entities);
        await _uow.SaveChangesAsync();
        return entities;
    }

    #endregion
}