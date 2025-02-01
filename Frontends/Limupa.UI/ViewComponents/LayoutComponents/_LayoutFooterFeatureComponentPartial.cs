using Limupa.DtoLayer.FeatureDtos;
using Limupa.DtoLayer.FeatureSliderDtos;
using Limupa.UI.Services.CatalogServices.FeatureServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Limupa.UI.ViewComponents.LayoutComponents
{
    public class _LayoutFooterFeatureComponentPartial:ViewComponent
    {
        private readonly IFeatureService featureService;

        public _LayoutFooterFeatureComponentPartial(IFeatureService featureService)
        {
            this.featureService = featureService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await featureService.GetAllFeatureAsync();
            return View(values);
        }
    }
}
