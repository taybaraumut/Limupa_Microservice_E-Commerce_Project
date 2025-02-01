using Limupa.DtoLayer.BasketDtos;
using Limupa.UI.Services.BasketServices;
using Microsoft.AspNetCore.Mvc;

namespace Limupa.UI.ViewComponents.ShoppingCartComponents
{
    public class ShoppingTotalCartPriceComponentPartial:ViewComponent
    {
        private readonly IBasketService basketService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ShoppingTotalCartPriceComponentPartial(IBasketService basketService,IHttpContextAccessor httpContextAccessor)
        {
            this.basketService = basketService;
            this.httpContextAccessor = httpContextAccessor;           
        }

        public async Task<IViewComponentResult> InvokeAsync(string code,int discountRate)
        {
            var id = httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value ?? string.Empty;

            if (id != "")
            {
                var basketTotal = await basketService.GetBasket();
                TempData["id"] = id;
                var totalPriceWithTax = ViewBag.totalPriceWithTax = basketTotal.TotalPrice / 100 * 10;
                var total = ViewBag.Total = basketTotal.TotalPrice + ViewBag.totalPriceWithTax;

                var totalNewPriceWithDiscount = total - (total / 100 * discountRate);

                ViewBag.totalPriceWithTax = totalPriceWithTax;

                ViewBag.totalNewPriceWithDiscount = totalNewPriceWithDiscount;

                ViewBag.Total = total;


                TempData["code"] = code;
                TempData["discountRate"] = discountRate;

                return View(basketTotal);
            }
            else
            {
                var basketTotal = new BasketTotalDto();
                TempData["id"] = id;
                return View(basketTotal);
            }
        }
    }
}
