
using Limupa.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using Limupa.Order.Application.Interfaces;
using Limupa.Order.Domain.Entities;

namespace Limupa.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers
{
    public class RemoveOrderDetailCommandHandler
    {
        private readonly IRepository<OrderDetail> repository;

        public RemoveOrderDetailCommandHandler(IRepository<OrderDetail> repository)
        {
            this.repository = repository;
        }

        public async Task Handle(RemoveOrderDetailCommand removeOrderDetailCommand)
        {
            var value = await repository.GetByIdAsync(removeOrderDetailCommand.Id);
            await repository.DeleteAsync(value);
        }
    }
}
