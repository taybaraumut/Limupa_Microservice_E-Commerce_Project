using Limupa.Order.Domain.Entities;
using System.Linq.Expressions;

namespace Limupa.Order.Application.Interfaces
{
    public interface IRepository<T> where T:class
    {
        Task<List<T>> GetAllAsync();
        Task CreateAsync(T t);
        Task UpdateAsync(T t);
        Task DeleteAsync(T t);
        Task<T> GetByIdAsync(int id);
        Task<T> GetByFilterAsync(Expression<Func<T,bool>>filter);
    }
}
