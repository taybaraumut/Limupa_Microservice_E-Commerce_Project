using Limupa.UI.Services.CatalogServices.ProductServices;
using Microsoft.AspNetCore.Mvc;

namespace Limupa.UI.ViewComponents.DefaultComponents
{
    public class DefaultGeneralProductMackbookModelComponentPartial:ViewComponent
    {
        private readonly IProductService productService;

        public DefaultGeneralProductMackbookModelComponentPartial(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await productService.GetProductMackbookLaptopModelAsync();
            return View(values);
        }
    }
}
