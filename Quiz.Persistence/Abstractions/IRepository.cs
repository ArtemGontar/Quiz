using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppQuiz.Persistence.Abstractions
{
    public interface IRepository<T>
    {
        Task<long> CountAsync();
        Task<long> CountAsync(ISpecification<T> specification);

        Task<bool> AnyAsync();
        Task<bool> AnyAsync(ISpecification<T> specification);

        Task<T> GetAsync(ISpecification<T> specification);

        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(ISpecification<T> specification);

        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(ISpecification<T> specification);

        Task<bool> SaveAsync(T entity);

        Task<bool> SaveBulkAsync(IEnumerable<T> entities);

        Task<bool> DeleteAsync(ISpecification<T> specification);

        Task<bool> DeleteManyAsync(ISpecification<T> specification);
    }
}
