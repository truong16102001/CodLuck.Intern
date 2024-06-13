using System.Linq.Expressions;

namespace P6.Infrastructure.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        Task Commit();
        Task Create(T entity);
        void Delete(T entity);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression = null);
        Task<T?> GetSingleAsync(Expression<Func<T, bool>> expression = null);
        void Update(T entity);
    }
}