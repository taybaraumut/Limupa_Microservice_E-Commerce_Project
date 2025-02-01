using Limupa.Cargo.DataAccessLayer.Abstract;
using Limupa.Cargo.DataAccessLayer.Concrete.Context;
using Microsoft.EntityFrameworkCore;

namespace Limupa.Cargo.DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        private readonly CargoContext cargoContext;

        public GenericRepository(CargoContext cargoContext)
        {
            this.cargoContext = cargoContext;
        }

        public async Task CreateAsync(T t)
        {
            cargoContext.Add(t);
            await cargoContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var value = await cargoContext.Set<T>().FindAsync(id);
            cargoContext.Remove(value);
            await cargoContext.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            var values = await cargoContext.Set<T>().ToListAsync();
            return values;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var value = await cargoContext.Set<T>().FindAsync(id);
            return value!;
        }

        public async Task UpdateAsync(T t)
        {
            cargoContext.Update(t);
            await cargoContext.SaveChangesAsync();
        }
    }
}
