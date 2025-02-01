using Limupa.DtoLayer.ProductDtos;
using Limupa.UI.Services.CatalogServices.ProductServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace Limupa.UI.ViewComponents.DefaultComponents
{
    public class DefaultFilterProductComponentPartial:ViewComponent
    {
        private readonly IProductService productService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public DefaultFilterProductComponentPartial(IProductService productService, IHttpContextAccessor httpContextAccessor)
        {
            this.productService = productService;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await productService.GetLastTenProductAsync();
            TempData["id"] = httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value ?? string.Empty;
            return View(values);
        }
    }
}
