using AutoMapper;
using Limupa.Cargo.BusinessLayer.Abstract;
using Limupa.Cargo.DtoLayer.Dtos.CargoOperationDtos;
using Limupa.Cargo.EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Limupa.Cargo.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoOperationsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICargoOperationService cargoOperationService;

        public CargoOperationsController(ICargoOperationService cargoOperationService, IMapper mapper)
        {
            this.cargoOperationService = cargoOperationService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> CargoOperationsList()
        {
            var values = await cargoOperationService.GetAllAsync();

            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCargoOperations(CreateCargoOperationDto createCargoOperationDto)
        {
            var value = mapper.Map<CargoOperation>(createCargoOperationDto);
            await cargoOperationService.CreateAsync(value);

            return Ok("Successful");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCargoOperations(int id)
        {
            await cargoOperationService.DeleteAsync(id);

            return Ok("Successful");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCargoOperationsById(int id)
        {
            var value = mapper.Map<GetCargoOperationByIdDto>(await cargoOperationService.GetByIdAsync(id));

            return Ok(value);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCargoOperations(UpdateCargoOperationDto updateCargoOperationDto)
        {
            var value = mapper.Map<CargoOperation>(updateCargoOperationDto);
            await cargoOperationService.UpdateAsync(value);

            return Ok("Successful");
        }
    }
}
