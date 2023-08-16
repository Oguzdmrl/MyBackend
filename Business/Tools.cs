using DataAccess.Context;
using DataAccess.Repo.UnitOfWorks;
using Microsoft.Extensions.DependencyInjection;

namespace Business;

public static class Tools
{
    static AppDBContext _dbContext = AppStatic.ServiceProviderInstance.GetRequiredService<AppDBContext>();
    private static UnitOfWork uow;
    public static UnitOfWork UOW
    {
        get
        {
            if (uow == null)
            {
                uow = new(_dbContext);
            }
            return uow;
        }
    }
}