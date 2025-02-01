using Limupa.DtoLayer.FeatureSliderDtos;
using Limupa.UI.Services.CatalogServices.FeatureSliderServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Limupa.UI.ViewComponents.DefaultComponents
{
    public class DefaultSliderComponentPartial:ViewComponent
    {
        private readonly  IFeatureSliderService featureSliderService;

        public DefaultSliderComponentPartial(IFeatureSliderService featureSliderService)
        {
            this.featureSliderService = featureSliderService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await featureSliderService.GetAllFeatureSliderAsync();
            return View(values);
        }
    }
}
