using AutoMapper;
using Limupa.Cargo.BusinessLayer.Abstract;
using Limupa.Cargo.DtoLayer.Dtos.CargoDetailDtos;
using Limupa.Cargo.EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Limupa.Cargo.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoDetailsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICargoDetailService cargoDetailService;

        public CargoDetailsController(ICargoDetailService cargoDetailService, IMapper mapper)
        {
            this.cargoDetailService = cargoDetailService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> CargoDetailsList()
        {
            var values = await cargoDetailService.GetAllAsync();

            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCargoDetails(CreateCargoDetailDto createCargoDetailDto)
        {
            var value = mapper.Map<CargoDetail>(createCargoDetailDto);
            await cargoDetailService.CreateAsync(value);

            return Ok("Successful");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCargoDetails(int id)
        {
            await cargoDetailService.DeleteAsync(id);

            return Ok("Successful");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCargoDetailsById(int id)
        {
            var value = mapper.Map<GetCargoDetailByIdDto>(await cargoDetailService.GetByIdAsync(id));

            return Ok(value);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCargoDetails(UpdateCargoDetailDto updateCargoDetailDto)
        {
            var value = mapper.Map<CargoDetail>(updateCargoDetailDto);
            await cargoDetailService.UpdateAsync(value);

            return Ok("Successful");
        }
    }
}
