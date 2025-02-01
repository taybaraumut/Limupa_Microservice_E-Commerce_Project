using Microsoft.AspNetCore.Mvc;

namespace Limupa.UI.ViewComponents.ShoppingCartComponents
{
    public class ShoppingCartPriceAndDiscountCodeComponentPartial:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string code,int discountRate)
        {
            TempData["code"] = code;
            TempData["discountRate"] = discountRate;
            return View();
        }
    }
}
