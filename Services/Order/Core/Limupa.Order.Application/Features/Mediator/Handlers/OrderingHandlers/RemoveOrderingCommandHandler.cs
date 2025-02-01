using Limupa.Order.Application.Features.Mediator.Commands.OrderingCommands;
using Limupa.Order.Application.Interfaces;
using Limupa.Order.Domain.Entities;
using MediatR;

namespace Limupa.Order.Application.Features.Mediator.Handlers.OrderingHandlers
{
    public class RemoveOrderingCommandHandler : IRequestHandler<RemoveOrderingCommand>
    {
        private readonly IRepository<Ordering> repository;

        public RemoveOrderingCommandHandler(IRepository<Ordering> repository)
        {
            this.repository = repository;
        }

        public async Task Handle(RemoveOrderingCommand request, CancellationToken cancellationToken)
        {
            var value = await repository.GetByIdAsync(request.Id);
            await repository.DeleteAsync(value);
        }
    }
}
