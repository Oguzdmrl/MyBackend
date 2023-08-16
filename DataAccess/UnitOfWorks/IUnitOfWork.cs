using DataAccess.Repository;
using Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.UnitOfWorks
{
    public interface IUnitOfWork
    {
        Task<IAsyncRepository<T, Guid>> AsyncRepositories<T>() where T : Entity<Guid>;
        Task SaveChangesAsync();
        void Dispose();
    }
}