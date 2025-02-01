using Limupa.UI.Services.CatalogServices.ProductServices;
using Microsoft.AspNetCore.Mvc;

namespace Limupa.UI.ViewComponents.ProductListComponents.ProductPhoneListComponents
{
    public class ProductApplePhoneListComponentPartial : ViewComponent
    {
        private readonly IProductService productService;

        public ProductApplePhoneListComponentPartial(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync(List<string> productName, List<decimal> productPrice, List<string> productModel, List<string> productInternalMemorySize)
        {
            if (productName.Count != 0 || productPrice.Count != 0 || productModel.Count != 0 || productInternalMemorySize.Count != 0)
            {
                var values = await productService.GetProductİphonePhoneListFilterAsync(productName, productPrice, productModel, productInternalMemorySize);
                return View(values);
            }
            else
            {
                var values = await productService.GetProductİphonePhoneListAsync();
                return View(values);
            }
        }
    }
}
