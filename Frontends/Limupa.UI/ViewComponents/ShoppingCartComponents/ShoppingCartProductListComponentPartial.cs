using Limupa.DtoLayer.BasketDtos;
using Limupa.UI.Services.BasketServices;
using Microsoft.AspNetCore.Mvc;

namespace Limupa.UI.ViewComponents.ShoppingCartComponents
{
    public class ShoppingCartProductListComponentPartial:ViewComponent
    {
        private readonly IBasketService basketService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ShoppingCartProductListComponentPartial(IBasketService basketService, IHttpContextAccessor httpContextAccessor)
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
                var basketItems = basketTotal.BasketItems;
                TempData["id_four"] = id;
                return View(basketItems);
            }
            else
            {
                TempData["id_four"] = id;
                var emptyBasketItems = new List<BasketItemDto>(); // Boş liste oluşturuyoruz
                return View(emptyBasketItems);
            }
           
        }
    }
}
