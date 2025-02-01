using Limupa.DtoLayer.BasketDtos;
using Limupa.UI.Services.BasketServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Limupa.UI.ViewComponents.LayoutComponents
{
    public class _LayoutHeaderMiniShoppingCartTotalPriceTwoComponentPartial : ViewComponent
    {
        private readonly IBasketService basketService;
        private readonly IHttpContextAccessor httpContextAccessor;
        public _LayoutHeaderMiniShoppingCartTotalPriceTwoComponentPartial(IBasketService basketService,
            IHttpContextAccessor httpContextAccessor)
        {
            this.basketService = basketService;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var id = httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value ?? string.Empty;

            if (id != "")
            {
                var basketTotal = await basketService.GetBasket();

                if (basketTotal == null || basketTotal.BasketItems == null)
                {
                    var emptyBasketItems = new BasketTotalDto(); // Boş liste oluşturuyoruz                
                    return View(emptyBasketItems); // Boş listeyi döndürüyoruz
                }

                TempData["id_three"] = id;

                return View(basketTotal);
            }
            else
            {
                TempData["id_three"] = id;
                var emptyBasketItems =new BasketTotalDto(); // Boş liste oluşturuyoruz                                
                return View(emptyBasketItems);
            }
        }
    }
}
