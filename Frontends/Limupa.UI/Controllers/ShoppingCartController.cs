using Limupa.DtoLayer.BasketDtos;
using Limupa.UI.Services.BasketServices;
using Limupa.UI.Services.CatalogServices.ProductServices;
using Limupa.UI.Services.DiscountServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Limupa.UI.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IProductService productService;
        private readonly IBasketService basketService;
        private readonly IHttpClientFactory httpClientFactory;

        public ShoppingCartController(IProductService productService, IBasketService basketService, IHttpClientFactory httpClientFactory)
        {
            this.productService = productService;
            this.basketService = basketService;
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(string code,int discountRate)
        {
            ViewData["Code"] = code;
            ViewData["DiscountRate"] = discountRate;

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AddBasketItem(string id)
        {
            var values = await productService.GetByIdProductAsync(id);
            var items = new BasketItemDto
            {
                ProductID = values.ProductID,
                ProductName = values.ProductName,
                ProductUrlSeo = values.ProductUrlSeo,
                ProductImageUrl = values.ProductImageUrl,
                SavedFileName = values.SavedFileName!,
                Price = values.ProductPrice,
                Quantity = 1
            };

            await basketService.AddBasketItem(items);
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> RemoveBasketItem(string id)
        {
            await basketService.RemoveBasketItem(id);
            return RedirectToAction("Index");
        }
    }
}
