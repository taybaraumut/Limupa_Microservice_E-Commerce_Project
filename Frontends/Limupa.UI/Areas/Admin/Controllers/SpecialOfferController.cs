using Limupa.DtoLayer.SpecialOfferDtos;
using Limupa.UI.Services.CatalogServices.SpecialOfferServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Limupa.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SpecialOfferController : Controller
    {
        private readonly ISpecialOfferService specialOfferService;

        public SpecialOfferController(ISpecialOfferService specialOfferService)
        {
            this.specialOfferService = specialOfferService;
        }

        public async Task<IActionResult> Index()
        {
            var values = await specialOfferService.GetAllSpecialOfferAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateSpecialOffer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSpecialOffer(CreateSpecialOfferDto createSpecialOfferDto)
        {
            await specialOfferService.CreateSpecialOfferAsync(createSpecialOfferDto); 
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateSpecialOffer(string id)
        {
            var value = await specialOfferService.GetByIdSpecialOfferAsync(id);
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSpecialOffer(UpdateSpecialOfferDto updateSpecialOfferDto)
        {
            await specialOfferService.UpdateSpecialOfferAsync(updateSpecialOfferDto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteSpecialOffer(string id)
        {
            await specialOfferService.DeleteSpecialOfferAsync(id);
            return RedirectToAction("Index");
        }
    }
}
