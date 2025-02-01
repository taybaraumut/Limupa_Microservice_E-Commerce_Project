using Limupa.DtoLayer.FeatureSliderDtos;
using Limupa.UI.Services.CatalogServices.FeatureSliderServices;
using Microsoft.AspNetCore.Mvc;


namespace Limupa.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FeatureSliderController : Controller
    {
        private readonly IFeatureSliderService featureSliderService;

        public FeatureSliderController(IFeatureSliderService featureSliderService)
        {
            this.featureSliderService = featureSliderService;
        }

        public async Task<IActionResult> Index()
        {
            var values = await featureSliderService.GetAllFeatureSliderAsync();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateFeatureSlider()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeatureSlider(CreateFeatureSliderDto createFeatureSliderDto)
        {
             await featureSliderService.CreateFeatureSliderAsync(createFeatureSliderDto);
             return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateFeatureSlider(string id)
        {
            var value = await featureSliderService.GetByIdFeatureSliderAsync(id);
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateFeatureSlider(UpdateFeatureSliderDto updateFeatureSliderDto)
        {
            await featureSliderService.UpdateFeatureSliderAsync(updateFeatureSliderDto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteFeatureSlider(string id)
        {
            await featureSliderService.DeleteFeatureSliderAsync(id);
            return RedirectToAction("Index");
        }
    }
}
