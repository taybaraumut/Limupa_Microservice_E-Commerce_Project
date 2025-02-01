using Bogus;
using Limupa.StoreLocation.Api.Context;
using Limupa.StoreLocation.Api.Dtos;
using Limupa.StoreLocation.Api.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Diagnostics;

namespace Limupa.StoreLocation.Api.Services
{
    public class StoreLocationService : IStoreLocationService
    {
        private readonly StoreLocationContext storeLocationContext;

        public StoreLocationService(StoreLocationContext storeLocationContext)
        {
            this.storeLocationContext = storeLocationContext;
        }

        public async Task CreateStoreLocationAsync(City city)
        {
            storeLocationContext.Add(city);
            await storeLocationContext.SaveChangesAsync();
        }

        public async Task DeleteStoreLocationAsync(int id)
        {
            var value = await storeLocationContext.Cities.FindAsync(id);
            storeLocationContext.Cities.Remove(value!);
            await storeLocationContext.SaveChangesAsync();
        }

        public IEnumerable<City> GetAllStoreLocation()
        {
            var values = storeLocationContext.Cities.Take(10).ToList();
            return values;
        }

        public async Task<City> GetByIdStoreLocationAsync(int id)
        {
            var value = await storeLocationContext.Cities.FindAsync(id);
            return value!;
        }

        public async Task UpdateStoreLocationAsync(City city)
        {
            storeLocationContext.Cities.Update(city);
            await storeLocationContext.SaveChangesAsync();
        }
    }
}
