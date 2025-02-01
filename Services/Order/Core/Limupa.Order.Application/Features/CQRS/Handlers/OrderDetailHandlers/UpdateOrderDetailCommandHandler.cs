using Limupa.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using Limupa.Order.Application.Interfaces;
using Limupa.Order.Domain.Entities;

namespace Limupa.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers
{
    public class UpdateOrderDetailCommandHandler
    {
        private readonly IRepository<OrderDetail> repository;

        public UpdateOrderDetailCommandHandler(IRepository<OrderDetail> repository)
        {
            this.repository = repository;
        }

        public async Task Handle(UpdateOrderDetailCommand updateOrderDetailCommand)
        {
            var value = await repository.GetByIdAsync(updateOrderDetailCommand.OrderDetailID);

            value.ProductName = updateOrderDetailCommand.ProductName;
            value.ProductPrice = updateOrderDetailCommand.ProductPrice;
            value.ProductTotalPrice = updateOrderDetailCommand.ProductTotalPrice;
            value.OrderingID = updateOrderDetailCommand.OrderingID;
            value.ProductAmount = updateOrderDetailCommand.ProductAmount;
            value.ProductID = updateOrderDetailCommand.ProductID;

            await repository.UpdateAsync(value);
        }
    }
}
