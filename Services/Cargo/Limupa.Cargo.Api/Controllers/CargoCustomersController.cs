using AutoMapper;
using Limupa.Cargo.BusinessLayer.Abstract;
using Limupa.Cargo.DtoLayer.Dtos.CargoCustomerDtos;
using Limupa.Cargo.EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Limupa.Cargo.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoCustomersController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICargoCustomerService cargoCustomerService;

        public CargoCustomersController(ICargoCustomerService cargoCustomerService, IMapper mapper)
        {
            this.cargoCustomerService = cargoCustomerService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> CargoCustomerList()
        {
            var values = await cargoCustomerService.GetAllAsync();

            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCargoCustomer(CreateCargoCustomerDto createCargoCustomerDto)
        {
            var value = mapper.Map<CargoCustomer>(createCargoCustomerDto);
            await cargoCustomerService.CreateAsync(value);

            return Ok("Successful");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCargoCustomer(int id)
        {
            await cargoCustomerService.DeleteAsync(id);

            return Ok("Successful");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCargoCustomerById(int id)
        {
            var value = mapper.Map<GetCargoCustomerByIdDto>(await cargoCustomerService.GetByIdAsync(id));

            return Ok(value);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCargoCustomer(UpdateCargoCustomerDto updateCargoCustomerDto)
        {
            var value = mapper.Map<CargoCustomer>(updateCargoCustomerDto);
            await cargoCustomerService.UpdateAsync(value);

            return Ok("Successful");
        }
    }
}
