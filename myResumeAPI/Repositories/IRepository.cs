using System.Linq.Expressions;

namespace myResumeAPI.Contracts
{
    public interface IRepository<T>
    {
        T Add(T entity);
        T Update(T entity);
        Task<T> FindAsync(long id);
        T Delete(T entity);
        Task<IEnumerable<T>> FindAllAsync();
        Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> predicate);
        Task SaveChangesAsync();
    }
}
