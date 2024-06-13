using System.Linq.Expressions;

namespace P6.Infrastructure.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        Task Commit();
        Task Create(T entity);
        Task Delete(T entity);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression = null);
        Task<T?> GetSingleAsync(Expression<Func<T, bool>> expression = null);
        Task Update(T entity);

        Task MultipleCreate(IEnumerable<T> entities);
        Task MultipleUpdate(IEnumerable<T> entities);
        Task MultipleDelete(IEnumerable<T> entities);
    }
}