
using Limupa.Order.Application.Features.CQRS.Queries.OrderDetailQueries;
using Limupa.Order.Application.Features.CQRS.Results.AddressResults;
using Limupa.Order.Application.Features.CQRS.Results.OrderDetailResults;
using Limupa.Order.Application.Interfaces;
using Limupa.Order.Domain.Entities;

namespace Limupa.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers
{
    public class GetOrderDetailByIdQueryHandler
    {
        private readonly IRepository<OrderDetail> repository;

        public GetOrderDetailByIdQueryHandler(IRepository<OrderDetail> repository)
        {
            this.repository = repository;
        }

        public async Task<GetOrderDetailByIdQueryResult> Handle(GetOrderDetailByIdQuery getOrderDetailByIdQuery)
        {
            var value = await repository.GetByIdAsync(getOrderDetailByIdQuery.Id);
            return new GetOrderDetailByIdQueryResult
            {
                OrderDetailID = value.OrderDetailID,
                OrderingID = value.OrderingID,
                ProductAmount = value.ProductAmount,
                ProductID = value.ProductID,
                ProductName = value.ProductName,
                ProductPrice = value.ProductPrice,
                ProductTotalPrice = value.ProductTotalPrice
            };
        }
    }
}
