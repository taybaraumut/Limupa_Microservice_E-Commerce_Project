using Limupa.UI.Services.CatalogServices.ProductServices;
using Microsoft.AspNetCore.Mvc;

namespace Limupa.UI.ViewComponents.ProductListComponents.ProductBluetoothListComponents
{
    public class ProductBluetoothGridListComponentPartial:ViewComponent
    {
        private readonly IProductService productService;
            
        public ProductBluetoothGridListComponentPartial(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync(List<string> productName,List<decimal> productPrice,List<string> productModel)
        {
            if (productName.Count != 0 || productPrice.Count != 0 || productModel.Count != 0)
            {
                var values = await productService.GetProductBluetoothListFilterAsync(productName, productPrice, productModel);
                return View(values);
            }
            else
            {
                var values = await productService.GetProductBluetoothListAsync();
                return View(values);
            }
        }
    }
}
