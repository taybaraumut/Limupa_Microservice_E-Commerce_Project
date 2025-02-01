using Limupa.StoreLocation.Api.Dtos;
using Limupa.StoreLocation.Api.Entities;

namespace Limupa.StoreLocation.Api.Services
{
    public interface IStoreLocationService
    {
        IEnumerable<City> GetAllStoreLocation();
        Task CreateStoreLocationAsync(City city);
        Task UpdateStoreLocationAsync(City city);
        Task DeleteStoreLocationAsync(int id);
        Task<City> GetByIdStoreLocationAsync(int id);
    }
}
