using Limupa.DtoLayer.CargoDtos.CargoCompanyDtos;
using Limupa.UI.Services.CargoServices.CargoCompanyServices;
using Microsoft.AspNetCore.Mvc;

namespace Limupa.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CargoCompanyController : Controller
    {
        private readonly ICargoCompanyService cargoCompanyService;

        public CargoCompanyController(ICargoCompanyService cargoCompanyService)
        {
            this.cargoCompanyService = cargoCompanyService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var values = await cargoCompanyService.GetAllCargoCompanyAsync();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateCargoCompany()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCargoCompany(CreateCargoCompanyDto createCargoCompanyDto)
        {
            await cargoCompanyService.CreateCargoCompanyAsync(createCargoCompanyDto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCargoCompany(string id)
        {
            var values = await cargoCompanyService.GetByIdCargoCompanyAsync(id);
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCargoCompany(UpdateCargoCompanyDto updateCargoCompanyDto)
        {
            await cargoCompanyService.UpdateCargoCompanyAsync(updateCargoCompanyDto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCargoCompany(string id)
        {
            await cargoCompanyService.DeleteCargoCompanyAsync(id);
            return RedirectToAction("Index");
        }
    }
}
