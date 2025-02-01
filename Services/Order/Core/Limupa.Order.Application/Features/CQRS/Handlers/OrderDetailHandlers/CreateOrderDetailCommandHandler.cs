
using Limupa.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using Limupa.Order.Application.Interfaces;
using Limupa.Order.Domain.Entities;

namespace Limupa.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers
{
    public class CreateOrderDetailCommandHandler
    {
        private readonly IRepository<OrderDetail> repository;

        public CreateOrderDetailCommandHandler(IRepository<OrderDetail> repository)
        {
            this.repository = repository;
        }

        public async Task Handle(CreateOrderDetailCommand createOrderDetailCommand)
        {
            await repository.CreateAsync(new OrderDetail
            {
                OrderingID = createOrderDetailCommand.OrderingID,
                ProductAmount = createOrderDetailCommand.ProductAmount,
                ProductID = createOrderDetailCommand.ProductID,
                ProductName = createOrderDetailCommand.ProductName,
                ProductTotalPrice = createOrderDetailCommand.ProductTotalPrice,
                ProductPrice = createOrderDetailCommand.ProductPrice
            });
        }
    }
}
