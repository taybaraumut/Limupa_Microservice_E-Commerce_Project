using Limupa.DtoLayer.ProductDetailDtos;
using Limupa.UI.Services.CatalogServices.ProductDetailService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Limupa.UI.ViewComponents.ProductDetailComponents
{
    public class ProductDetailDescriptionComponentPartial : ViewComponent
    {
        private readonly IProductDetailService productDetailService;

        public ProductDetailDescriptionComponentPartial(IProductDetailService productDetailService)
        {
            this.productDetailService = productDetailService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            var values = await productDetailService.GetProductDetailByProductIdAsync(id);
            return View(values);
        }
    }
}
