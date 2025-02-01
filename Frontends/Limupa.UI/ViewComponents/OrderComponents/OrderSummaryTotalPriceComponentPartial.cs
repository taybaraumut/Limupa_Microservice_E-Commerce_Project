using Limupa.UI.Services.BasketServices;
using Microsoft.AspNetCore.Mvc;

namespace Limupa.UI.ViewComponents.OrderComponents
{
    public class OrderSummaryTotalPriceComponentPartial:ViewComponent
    {
        private readonly IBasketService basketService;

        public OrderSummaryTotalPriceComponentPartial(IBasketService basketService)
        {
            this.basketService = basketService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var basketTotal = await basketService.GetBasket();
            return View(basketTotal);
        }
    }
}
