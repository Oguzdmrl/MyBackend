using DataAccess.Context;
using DataAccess.Repository;
using DataAccess.UnitOfWorks;
using Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repo.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDBContext _context;
        private readonly Dictionary<Type, object> _repositories;

        public Dictionary<Type, object> Repositories => _repositories;

        public UnitOfWork(AppDBContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
        }

        public async Task<IAsyncRepository<T, Guid>> AsyncRepositories<T>() where T : Entity<Guid>
        {
            if (!Repositories.Keys.Contains(typeof(T)))
            {
                var repository = new EfRepositoryBase<T, Guid, AppDBContext>(_context, this);
                Repositories.Add(typeof(T), repository);
                return await Task.FromResult((IAsyncRepository<T, Guid>)repository);
            }
            return await Task.FromResult((IAsyncRepository<T, Guid>)Repositories[typeof(T)]);
        }

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        public async void Dispose()
        {
            await _context.DisposeAsync();
            GC.SuppressFinalize(this);
        }
    }
}