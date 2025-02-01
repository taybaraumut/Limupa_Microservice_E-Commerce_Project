using Limupa.Order.Application.Features.CQRS.Results.OrderDetailResults;
using Limupa.Order.Application.Interfaces;
using Limupa.Order.Domain.Entities;

namespace Limupa.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers
{
    public class GetOrderDetailQueryHandler
    {
        private readonly IRepository<OrderDetail> repository;

        public GetOrderDetailQueryHandler(IRepository<OrderDetail> repository)
        {
            this.repository = repository;
        }

        public async Task<List<GetOrderDetailQueryResult>> Handle()
        {
            var values = await repository.GetAllAsync();
            return values.Select(x => new GetOrderDetailQueryResult
            {
                OrderDetailID = x.OrderDetailID,
                OrderingID = x.OrderingID,
                ProductAmount = x.ProductAmount,
                ProductID = x.ProductID,
                ProductName = x.ProductName,
                ProductPrice = x.ProductPrice,
                ProductTotalPrice = x.ProductTotalPrice

            }).ToList();
        }
    }
}
