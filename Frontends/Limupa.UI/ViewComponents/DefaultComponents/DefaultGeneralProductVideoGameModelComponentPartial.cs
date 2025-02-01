using Limupa.UI.Services.CatalogServices.ProductServices;
using Microsoft.AspNetCore.Mvc;

namespace Limupa.UI.ViewComponents.DefaultComponents
{
    public class DefaultGeneralProductVideoGameModelComponentPartial:ViewComponent
    {
        private readonly IProductService productService;

        public DefaultGeneralProductVideoGameModelComponentPartial(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await productService.GetProductVideoGameModelAsync();
            return View(values);
        }
    }
}
