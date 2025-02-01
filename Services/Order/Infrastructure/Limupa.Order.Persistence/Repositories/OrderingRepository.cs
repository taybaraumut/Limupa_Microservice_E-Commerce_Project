
using Limupa.Order.Application.Interfaces;
using Limupa.Order.Domain.Entities;
using Limupa.Order.Persistence.Context;

namespace Limupa.Order.Persistence.Repositories
{
    public class OrderingRepository : IOrderingRepository
    {
        private readonly OrderContext orderContext;

        public OrderingRepository(OrderContext orderContext)
        {
            this.orderContext = orderContext;
        }

        public List<Ordering> GetOrderingsByUserId(string id)
        {
            var values = orderContext.Orderings.Where(x => x.UserID == id).ToList();
            return values;
        }
    }
}
