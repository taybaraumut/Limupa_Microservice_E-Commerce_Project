using Limupa.DtoLayer.BasketDtos;
using Limupa.UI.Services.BasketServices;
using Microsoft.AspNetCore.Mvc;

namespace Limupa.UI.ViewComponents.LayoutComponents
{
    public class _LayoutHeaderMiniShoppingCartTotalPriceComponentPartial : ViewComponent
    {
        private readonly IBasketService basketService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public _LayoutHeaderMiniShoppingCartTotalPriceComponentPartial(IBasketService basketService, IHttpContextAccessor httpContextAccessor)
        {
            this.basketService = basketService;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var id = httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value ?? string.Empty;

            if (id != null)
            {
                var basketTotal = await basketService.GetBasket();

                if (basketTotal == null || basketTotal.BasketItems == null)
                {
                    var emptyBasketItems = new BasketTotalDto();
                    // Boş liste oluşturuyoruz
                    return View(emptyBasketItems); // Boş listeyi döndürüyoruz
                }

                return View(basketTotal);
            }
            else
            {
                TempData["id_two"] = id;
                var emptyBasketItems = new BasketTotalDto();
                // Boş liste oluşturuyoruz
                return View(emptyBasketItems);
            }
        }
    }
}
