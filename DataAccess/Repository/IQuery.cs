namespace DataAccess.Repository;
public interface IQuery<T>
{
    IQueryable<T> Query();
}