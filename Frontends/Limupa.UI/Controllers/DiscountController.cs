using Limupa.UI.Services.BasketServices;
using Limupa.UI.Services.DiscountServices;
using Microsoft.AspNetCore.Mvc;

namespace Limupa.UI.Controllers
{
    public class DiscountController : Controller
    {
        private readonly IDiscountService discountService;
        private readonly IBasketService basketService;

        public DiscountController(IDiscountService discountService, IBasketService basketService)
        {
            this.discountService = discountService;
            this.basketService = basketService;
        }
        [NonAction]
        public PartialViewResult ConfirmDiscountCouponCode()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDiscountCouponCode(string code)
        {
            var values = await discountService.GetDiscountCouponCountRateAsync(code);

            var basketValues = await basketService.GetBasket();
            var totalPriceWithTax = basketValues.TotalPrice + basketValues.TotalPrice / 100 * 10;
            var totalNewPriceWithDiscount = totalPriceWithTax - (totalPriceWithTax / 100 * values);
            ViewBag.totalPriceWithTax = totalPriceWithTax;
            ViewBag.totalNewPriceWithDiscount = totalNewPriceWithDiscount;

            return RedirectToAction("Index", "ShoppingCart", new {code = code,discountRate = values});
        }
    }
}
