using Limupa.UI.Services.CatalogServices.ProductServices;
using Microsoft.AspNetCore.Mvc;

namespace Limupa.UI.ViewComponents.DefaultComponents
{
    public class DefaultFilterProductTwoComponentPartial:ViewComponent
    {
        private readonly IProductService productService;
        private readonly IHttpContextAccessor httpContextAccessor;
        public DefaultFilterProductTwoComponentPartial(IProductService productService,IHttpContextAccessor httpContextAccessor)
        {
            this.productService = productService;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await productService.GetTenDataByProductPersonelCareCategoryAsync();
            TempData["id"] = httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value ?? string.Empty; ;
            return View(values);
        }
    }
}
