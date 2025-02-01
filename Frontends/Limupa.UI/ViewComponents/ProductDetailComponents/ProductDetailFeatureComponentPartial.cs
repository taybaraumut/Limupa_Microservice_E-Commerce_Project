using Limupa.DtoLayer.AboutDtos;
using Limupa.DtoLayer.ProductDtos;
using Limupa.UI.Services.CatalogServices.ProductServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Limupa.UI.ViewComponents.ProductDetailComponents
{
    public class ProductDetailFeatureComponentPartial : ViewComponent
    {
        private readonly IProductService productService;

        public ProductDetailFeatureComponentPartial(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            var value = await productService.GetByIdProductAsync(id);
            return View(value);
        }
    }
}
