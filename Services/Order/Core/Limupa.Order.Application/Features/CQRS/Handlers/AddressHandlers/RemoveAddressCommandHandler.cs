using Limupa.Order.Application.Features.CQRS.Commands.AddressCommands;
using Limupa.Order.Application.Interfaces;
using Limupa.Order.Domain.Entities;

namespace Limupa.Order.Application.Features.CQRS.Handlers.AddressHandlers
{
    public class RemoveAddressCommandHandler
    {
        private readonly IRepository<Address> repository;

        public RemoveAddressCommandHandler(IRepository<Address> repository)
        {
            this.repository = repository;
        }

        public async Task Handle(RemoveAddressCommand removeAddressCommand)
        {
            var value = await repository.GetByIdAsync(removeAddressCommand.Id);
            await repository.DeleteAsync(value);
        } 
    }
}
