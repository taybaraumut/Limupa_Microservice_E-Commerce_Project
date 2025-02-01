
using System.Linq.Expressions;

namespace Limupa.Cargo.DataAccessLayer.Abstract
{
    public interface IGenericDal<T>  where T:class
    {
        Task<List<T>> GetAllAsync();
        Task CreateAsync(T t);
        Task DeleteAsync(int id);
        Task UpdateAsync(T t);
        Task<T> GetByIdAsync(int id);
    }
}
