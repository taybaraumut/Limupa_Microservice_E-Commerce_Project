using Limupa.Order.Application.Interfaces;
using Limupa.Order.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Limupa.Order.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly OrderContext orderContext;

        public Repository(OrderContext orderContext)
        {
            this.orderContext = orderContext;
        }

        public async Task CreateAsync(T t)
        {
            orderContext.Set<T>().Add(t);
            await orderContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T t)
        {
           orderContext.Remove(t);
           await orderContext.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            var values = await orderContext.Set<T>().ToListAsync();
            return values;
        }

        public async Task<T> GetByFilterAsync(Expression<Func<T, bool>> filter)
        {
            var value = await orderContext.Set<T>().FindAsync(filter);
            return value!;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var value = await orderContext.Set<T>().FindAsync(id);
            return value!;
        }

        public async Task UpdateAsync(T t)
        {
            orderContext.Set<T>().Update(t);
            await orderContext.SaveChangesAsync();
        }
    }
}
