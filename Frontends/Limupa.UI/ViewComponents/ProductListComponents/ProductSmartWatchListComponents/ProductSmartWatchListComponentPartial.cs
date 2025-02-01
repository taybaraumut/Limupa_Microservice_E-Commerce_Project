using Limupa.UI.Services.CatalogServices.ProductServices;
using Microsoft.AspNetCore.Mvc;

namespace Limupa.UI.ViewComponents.ProductListComponents.ProductSmartWatchListComponents
{
    public class ProductSmartWatchListComponentPartial : ViewComponent
    {
        private readonly IProductService productService;

        public ProductSmartWatchListComponentPartial(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync(List<string> productName, List<decimal> productPrice, List<string> productColor)
        {
            if (productName.Count != 0 || productPrice.Count != 0 || productColor.Count != 0)
            {
                var values = await productService.GetProductSmartWatchListFilterAsync(productName, productPrice, productColor);
                return View(values);
            }
            else
            {
                var values = await productService.GetProductSmartWatchListAsync();
                return View(values);
            }
        }
    }
}
