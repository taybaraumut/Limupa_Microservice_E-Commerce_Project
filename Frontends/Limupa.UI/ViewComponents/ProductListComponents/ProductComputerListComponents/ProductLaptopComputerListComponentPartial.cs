using Limupa.UI.Services.CatalogServices.ProductServices;
using Microsoft.AspNetCore.Mvc;

namespace Limupa.UI.ViewComponents.ProductListComponents.ProductComputerListComponents
{
    public class ProductLaptopComputerListComponentPartial : ViewComponent
    {
        private readonly IProductService productService;

        public ProductLaptopComputerListComponentPartial(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync(List<string> productName, List<decimal> productPrice, List<string> productProcessModel, List<string> productGrapichCardModel, List<string> productMemoryRam)
        {
            if (productName.Count != 0 || productPrice.Count != 0 || productProcessModel.Count != 0 || productGrapichCardModel.Count != 0 || productMemoryRam.Count != 0)
            {
                var values = await productService.GetProductLaptopComputerListFilterAsync(productName, productPrice, productProcessModel, productGrapichCardModel, productMemoryRam);
                return View(values);
            }
            else
            {
                var values = await productService.GetProductLaptopComputerListAsync();
                return View(values);
            }
        }
    }
}
