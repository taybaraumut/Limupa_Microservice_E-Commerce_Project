using Limupa.UI.Services.CatalogServices.ProductServices;
using Microsoft.AspNetCore.Mvc;

namespace Limupa.UI.ViewComponents.ProductListComponents.ProductPhoneListComponents
{
    public class ProductTecnoCamonPhoneGridListComponentPartial:ViewComponent
    {
        private readonly IProductService productService;

        public ProductTecnoCamonPhoneGridListComponentPartial(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync(List<string> productName, List<decimal> productPrice, List<string> productModel, List<string> productInternalMemorySize, List<string> productMobileRamSize)
        {

            if (productName.Count != 0 || productPrice.Count != 0 || productModel.Count != 0 || productInternalMemorySize.Count != 0 || productMobileRamSize.Count != 0)
            {
                var values = await productService.GetProductTecnoCamonPhoneListFilterAsync(productName, productPrice, productModel, productInternalMemorySize, productMobileRamSize);
                return View(values);
            }
            else
            {
                var values = await productService.GetProductTecnoCamonPhoneListAsync();
                return View(values);
            }
        }
    }
}
