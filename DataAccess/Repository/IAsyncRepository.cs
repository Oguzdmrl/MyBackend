using Core.Results;
using Entities.Base;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace DataAccess.Repository;

public interface IAsyncRepository<TEntity, TKey> : IQuery<TEntity>
    where TEntity : Entity<TKey>
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