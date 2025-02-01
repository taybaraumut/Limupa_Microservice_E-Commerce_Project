using Limupa.UI.Services.CatalogServices.ProductServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Limupa.UI.ViewComponents.DefaultComponents
{
    public class DefaultElectronicDeviceProductComponentPartial:ViewComponent
    {
        private readonly IProductService productService;
        private readonly IHttpContextAccessor httpContextAccessor;
        public DefaultElectronicDeviceProductComponentPartial(IProductService productService, IHttpContextAccessor httpContextAccessor)
        {
            this.productService = productService;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await productService.GetProductByElectronicDeviceCategoryAsync();
            TempData["id"] = httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value ?? string.Empty;
            return View(values);
        }
    }
}
