using Limupa.UI.Services.CatalogServices.ProductServices;
using Microsoft.AspNetCore.Mvc;

namespace Limupa.UI.ViewComponents.DefaultComponents
{
    public class DefaultGeneralProductİpohneModelComponentPartial:ViewComponent
    {
        private readonly IProductService productService;

        public DefaultGeneralProductİpohneModelComponentPartial(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await productService.GetProductİpohnePhoneModelAsync();
            return View(values);
        }
    }
}
