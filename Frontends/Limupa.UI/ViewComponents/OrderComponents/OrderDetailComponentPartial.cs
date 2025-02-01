using Microsoft.AspNetCore.Mvc;

namespace Limupa.UI.ViewComponents.OrderComponents
{
    public class OrderDetailComponentPartial:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
