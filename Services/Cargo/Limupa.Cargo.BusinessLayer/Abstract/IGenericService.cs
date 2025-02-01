
namespace Limupa.Cargo.BusinessLayer.Abstract
{
    public interface IGenericService<T> where T:class
    {
        Task<List<T>> GetAllAsync();
        Task CreateAsync(T t);
        Task DeleteAsync(int id);
        Task UpdateAsync(T t);
        Task<T> GetByIdAsync(int id);
    }
}
