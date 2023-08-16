using Core.Results;
using Entities.Base;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Business.Repo.BaseRepo;

public interface IBaseRepository<TEntity> where TEntity : Entity<Guid>
{

    Task<ResponseDataResult<TEntity?>> GetAsnyc(
         Expression<Func<TEntity, bool>> predicate,
         Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
         bool withDeleted = false,
         bool enableTracking = true,
         CancellationToken cancellationToken = default
         );
    Task<ResponseDataResult<TEntity?>> GetListAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<bool> AnyAsync(
       Expression<Func<TEntity, bool>>? predicate = null,
       bool withDeleted = false,
       bool enableTracking = true,
       CancellationToken cancellationToken = default
   );
    Task<TEntity> AddAsync(TEntity entity);
    Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<ICollection<TEntity>> UpdateRangeAsync(ICollection<TEntity> entities);
    Task<TEntity> DeleteAsync(TEntity entity);
    Task<ICollection<TEntity>> DeleteRangeAsync(ICollection<TEntity> entities);
}
public partial class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Entity<Guid>
{
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        return await Task.FromResult(await Tools.UOW.AsyncRepositories<TEntity>().Result.AddAsync(entity));
    }
    public async Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities)
    {
        return await Task.FromResult(await Tools.UOW.AsyncRepositories<TEntity>().Result.AddRangeAsync(entities));
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? predicate = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(await Tools.UOW.AsyncRepositories<TEntity>().Result.AnyAsync(predicate, withDeleted, enableTracking, cancellationToken));
    }

    public async Task<TEntity> DeleteAsync(TEntity entity)
    {
        var resEntity = Tools.UOW.AsyncRepositories<TEntity>().Result.GetAsnyc(x => x.Id == entity.Id).Result.ResponseModel;
        return await Task.FromResult(await Tools.UOW.AsyncRepositories<TEntity>().Result.DeleteAsync(resEntity));
    }

    public async Task<ICollection<TEntity>> DeleteRangeAsync(ICollection<TEntity> entities)
    {
        return await Task.FromResult(await Tools.UOW.AsyncRepositories<TEntity>().Result.DeleteRangeAsync(entities));
    }

    public async Task<ResponseDataResult<TEntity?>> GetAsnyc(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(await Tools.UOW.AsyncRepositories<TEntity>().Result.GetAsnyc(predicate, include, withDeleted, enableTracking, cancellationToken));
    }

    public async Task<ResponseDataResult<TEntity?>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(await Tools.UOW.AsyncRepositories<TEntity>().Result.GetListAsync(predicate, include, withDeleted, enableTracking, cancellationToken));
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        return await Task.FromResult(await Tools.UOW.AsyncRepositories<TEntity>().Result.UpdateAsync(entity));
    }

    public async Task<ICollection<TEntity>> UpdateRangeAsync(ICollection<TEntity> entities)
    {
        return await Task.FromResult(await Tools.UOW.AsyncRepositories<TEntity>().Result.UpdateRangeAsync(entities));
    }
}