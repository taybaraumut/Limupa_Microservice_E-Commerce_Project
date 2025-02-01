using Limupa.Order.Application.Features.CQRS.Commands.AddressCommands;
using Limupa.Order.Application.Features.CQRS.Handlers.AddressHandlers;
using Limupa.Order.Application.Features.CQRS.Queries.AddressQueries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Limupa.Order.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly GetAddressQueryHandler getAddressQueryHandler;
        private readonly GetAddressByIdQueryHandler getAddressByIdQueryHandler;
        private readonly CreateAddressCommandHandler createAddressCommandHandler;
        private readonly UpdateAddressCommandHandler updateAddressCommandHandler;
        private readonly RemoveAddressCommandHandler removeAddressCommandHandler;

        public AddressesController(GetAddressQueryHandler getAddressQueryHandler, GetAddressByIdQueryHandler getAddressByIdQueryHandler,
                                   CreateAddressCommandHandler createAddressCommandHandler, UpdateAddressCommandHandler updateAddressCommandHandler,
                                   RemoveAddressCommandHandler removeAddressCommandHandler)
        {
            this.getAddressQueryHandler = getAddressQueryHandler;
            this.getAddressByIdQueryHandler = getAddressByIdQueryHandler;
            this.createAddressCommandHandler = createAddressCommandHandler;
            this.updateAddressCommandHandler = updateAddressCommandHandler;
            this.removeAddressCommandHandler = removeAddressCommandHandler;
        }

        [HttpGet]
        public async Task<IActionResult> AddressList()
        {
            var values = await getAddressQueryHandler.Handle();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddressById(int id)
        {
            var value = await getAddressByIdQueryHandler.Handle(new GetAddressByIdQuery(id));
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAddress(CreateAddressCommand command)
        {
            await createAddressCommandHandler.Handle(command);
            return Ok("Successful");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAddress(UpdateAddressCommand command)
        {
            await updateAddressCommandHandler.Handle(command);
            return Ok("Successful");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAddress(int id)
        {
            await removeAddressCommandHandler.Handle(new RemoveAddressCommand(id));
            return Ok("Successful");
        }

    }
}
