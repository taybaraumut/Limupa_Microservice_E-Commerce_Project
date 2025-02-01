using Limupa.UI.Services.CatalogServices.ProductServices;
using Microsoft.AspNetCore.Mvc;

namespace Limupa.UI.ViewComponents.ProductListComponents.ProductMemoryCardListComponents
{
    public class ProductMemoryCardListComponentPartial:ViewComponent
    {
        private readonly IProductService productService;

        public ProductMemoryCardListComponentPartial(IProductService productService)
        {
            this.productService = productService;
        }
        public async Task<IViewComponentResult> InvokeAsync(List<string> productName, List<decimal> productPrice, List<string> productModel, List<string> productStorage)
        {
            if (productName.Count != 0 || productPrice.Count != 0 || productModel.Count != 0 || productStorage.Count != 0)
            {
                var values = await productService.GetProductMemoryCardListFilterAsync(productName, productPrice, productModel, productStorage);
                return View(values);
            }
            else
            {
                var values = await productService.GetProductMemoryCardListAsync();
                return View(values);
            }
        }
    }
}
