using Limupa.DtoLayer.BasketDtos;
using Limupa.UI.Services.BasketServices;
using Limupa.UI.Services.CatalogServices.ProductServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Limupa.UI.ViewComponents.LayoutComponents
{
    public class _LayoutHeaderMiniShoppingCartComponentPartial : ViewComponent
    {
        private readonly IBasketService basketService;
        private readonly IProductService productService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public _LayoutHeaderMiniShoppingCartComponentPartial(IBasketService basketService, IHttpContextAccessor httpContextAccessor,
            IProductService productService)
        {
            this.basketService = basketService;
            this.httpContextAccessor = httpContextAccessor;
            this.productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var id = httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value ?? string.Empty;
            
            if (id != "")
            {
                var basketTotal = await basketService.GetBasket();
                if (basketTotal == null || basketTotal.BasketItems == null)
                {
                    var emptyBasketItems = new List<BasketItemDto>(); // Boş liste oluşturuyoruz
                    return View(emptyBasketItems); // Boş listeyi döndürüyoruz
                }

                TempData["id_one"] = id;

                return View(basketTotal.BasketItems);
            }
            else
            {
                TempData["id_one"] = id;
                var emptyBasketItems = new List<BasketItemDto>(); // Boş liste oluşturuyoruz
                return View(emptyBasketItems);
            }
        }
    }
}
