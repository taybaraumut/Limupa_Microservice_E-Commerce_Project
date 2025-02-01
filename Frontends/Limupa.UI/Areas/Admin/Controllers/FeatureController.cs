using Limupa.DtoLayer.FeatureDtos;
using Limupa.UI.Services.CatalogServices.FeatureServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Limupa.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FeatureController : Controller
    {
        private readonly IFeatureService featureService;

        public FeatureController(IFeatureService featureService)
        {
            this.featureService = featureService;
        }

        public async Task<IActionResult> Index()
        {
            var values = await featureService.GetAllFeatureAsync();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateFeature()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeature(CreateFeatureDto createFeatureDto)
        {
            await featureService.CreateFeatureAsync(createFeatureDto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateFeature(string id)
        {
            var value = await featureService.GetByIdFeatureAsync(id);
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateFeature(UpdateFeatureDto updateFeatureDto)
        {
            await featureService.UpdateFeatureAsync(updateFeatureDto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteFeature(string id)
        {
            await featureService.DeleteFeatureAsync(id);
            return RedirectToAction("Index");
        }
    }
}
