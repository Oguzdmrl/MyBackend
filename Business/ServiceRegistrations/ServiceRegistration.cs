using Business.Repo.BaseRepo;
using DataAccess.Context;
using DataAccess.Repo.UnitOfWorks;
using DataAccess.UnitOfWorks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Business.ServiceRegistrations
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddEndpointsApiExplorer();
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));

            #region Todos refactor
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>)); // Düzenlenicek
            services.AddTransient(typeof(BaseRepository<>)); // Düzenlenicek
            #endregion
           
            services.AddHttpContextAccessor();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

            services.AddDbContext<AppDBContext>(x => x.UseNpgsql(configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);

            return services;
        }
    }
}