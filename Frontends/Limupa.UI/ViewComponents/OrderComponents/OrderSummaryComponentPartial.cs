using Limupa.UI.Services.BasketServices;
using Microsoft.AspNetCore.Mvc;

namespace Limupa.UI.ViewComponents.OrderComponents
{
    public class OrderSummaryComponentPartial:ViewComponent
    {
        private readonly IBasketService basketService;

        public OrderSummaryComponentPartial(IBasketService basketService)
        {
            this.basketService = basketService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var basketItem = await basketService.GetBasket();
            var value = basketItem.BasketItems;
            return View(value);
        }
    }
}
