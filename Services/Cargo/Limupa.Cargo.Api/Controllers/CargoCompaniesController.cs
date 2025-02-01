using AutoMapper;
using Limupa.Cargo.BusinessLayer.Abstract;
using Limupa.Cargo.DtoLayer.Dtos.CargoCompanyDtos;
using Limupa.Cargo.EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Limupa.Cargo.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoCompaniesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICargoCompanyService cargoCompanyService;

        public CargoCompaniesController(ICargoCompanyService cargoCompanyService, IMapper mapper)
        {
            this.cargoCompanyService = cargoCompanyService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> CargoCompanyList()
        {
            var values = await cargoCompanyService.GetAllAsync();

            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCargoCompany(CreateCargoCompanyDto createCargoCompanyDto)
        {
            var value = mapper.Map<CargoCompany>(createCargoCompanyDto);
            await cargoCompanyService.CreateAsync(value);

            return Ok("Successful");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCargoCompany(int id)
        {         
            await cargoCompanyService.DeleteAsync(id);

            return Ok("Successful");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCargoCompanyById(int id)
        {
            var value = mapper.Map<GetCargoCompanyByIdDto>(await cargoCompanyService.GetByIdAsync(id));

            return Ok(value);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCargoCompany(UpdateCargoCompanyDto updateCargoCompanyDto)
        {
            var value = mapper.Map<CargoCompany>(updateCargoCompanyDto);
            await cargoCompanyService.UpdateAsync(value);

            return Ok("Successful");
        }
    }
}
