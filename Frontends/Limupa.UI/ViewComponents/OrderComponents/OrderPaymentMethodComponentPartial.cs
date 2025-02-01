using Microsoft.AspNetCore.Mvc;

namespace Limupa.UI.ViewComponents.OrderComponents
{
    public class OrderPaymentMethodComponentPartial:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
