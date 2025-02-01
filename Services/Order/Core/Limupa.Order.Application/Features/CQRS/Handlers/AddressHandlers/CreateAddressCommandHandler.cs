using Limupa.Order.Application.Features.CQRS.Commands.AddressCommands;
using Limupa.Order.Application.Interfaces;
using Limupa.Order.Domain.Entities;

namespace Limupa.Order.Application.Features.CQRS.Handlers.AddressHandlers
{
    public class CreateAddressCommandHandler
    {
        private readonly IRepository<Address> repository;

        public CreateAddressCommandHandler(IRepository<Address> repository)
        {
            this.repository = repository;
        }

        public async Task Handle(CreateAddressCommand createAddressCommand)
        {
            await repository.CreateAsync(new Address
            {              
                City = createAddressCommand.City,
                DetailOne = createAddressCommand.DetailOne,
                DetailTwo = createAddressCommand.DetailTwo,
                District = createAddressCommand.District,
                UserID = createAddressCommand.UserID,
                Country = createAddressCommand.Country,
                Description = createAddressCommand.Description,
                Email = createAddressCommand.Email,
                Name = createAddressCommand.Name,
                Phone = createAddressCommand.Phone,
                Surname = createAddressCommand.Surname,
                ZipCode = createAddressCommand.ZipCode
            });
        }
    }
}
