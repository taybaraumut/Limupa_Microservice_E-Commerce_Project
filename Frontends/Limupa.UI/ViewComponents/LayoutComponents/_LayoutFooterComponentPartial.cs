using Limupa.DtoLayer.AboutDtos;
using Limupa.UI.Services.CatalogServices.AboutServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Limupa.UI.ViewComponents.LayoutComponents
{
    public class _LayoutFooterComponentPartial:ViewComponent
    {
        private readonly IAboutService aboutService;

        public _LayoutFooterComponentPartial(IAboutService aboutService)
        {
            this.aboutService = aboutService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await aboutService.GetAllAboutAsync();
            return View(values);
        }
    }
}
