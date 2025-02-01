using Microsoft.AspNetCore.Mvc;

namespace Limupa.UI.ViewComponents.ProductDetailComponents
{
    public class ProductDetailCouponCodeComponentPartial : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
