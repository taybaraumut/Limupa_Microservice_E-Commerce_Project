

using Limupa.Order.Application.Features.CQRS.Commands.AddressCommands;
using Limupa.Order.Application.Interfaces;
using Limupa.Order.Domain.Entities;

namespace Limupa.Order.Application.Features.CQRS.Handlers.AddressHandlers
{
    public class UpdateAddressCommandHandler
    {
        private readonly IRepository<Address> repository;

        public UpdateAddressCommandHandler(IRepository<Address> repository)
        {
            this.repository = repository;
        }

        public async Task Handle(UpdateAddressCommand updateAddressCommand)
        {
            var values = await repository.GetByIdAsync(updateAddressCommand.AddressID);
            values.DetailOne = updateAddressCommand.Detail;
            values.District = updateAddressCommand.District;
            values.City = updateAddressCommand.City;
            values.UserID = updateAddressCommand.UserID;

            await repository.UpdateAsync(values);
        }
    }
}
