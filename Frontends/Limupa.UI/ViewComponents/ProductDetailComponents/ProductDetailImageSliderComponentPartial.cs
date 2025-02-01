using Limupa.DtoLayer.ProductImageDtos;
using Limupa.UI.Services.CatalogServices.ProductImageServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Limupa.UI.ViewComponents.ProductDetailComponents
{
    public class ProductDetailImageSliderComponentPartial : ViewComponent
    {
        private readonly IProductImageService productImageService;

        public ProductDetailImageSliderComponentPartial(IProductImageService productImageService)
        {
            this.productImageService = productImageService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            var value = await productImageService.GetProductImageByProductIdAsync(id);
            return View(value);
        }
    }
}
