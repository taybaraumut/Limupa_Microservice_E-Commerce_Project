
using Limupa.Order.Application.Features.Mediator.Commands.OrderingCommands;
using Limupa.Order.Application.Interfaces;
using Limupa.Order.Domain.Entities;
using MediatR;

namespace Limupa.Order.Application.Features.Mediator.Handlers.OrderingHandlers
{
    public class UpdateOrderingCommandHandler : IRequestHandler<UpdateOrderingCommand>
    {
        private readonly IRepository<Ordering> repository;

        public UpdateOrderingCommandHandler(IRepository<Ordering> repository)
        {
            this.repository = repository;
        }

        public async Task Handle(UpdateOrderingCommand request, CancellationToken cancellationToken)
        {
            var value = await repository.GetByIdAsync(request.OrderingID);

            value.OrderingID = request.OrderingID;
            value.OrderDate = request.OrderDate;
            value.UserID = request.UserID;
            value.TotalPrice = request.TotalPrice;

            await repository.UpdateAsync(value);
        }
    }
}
