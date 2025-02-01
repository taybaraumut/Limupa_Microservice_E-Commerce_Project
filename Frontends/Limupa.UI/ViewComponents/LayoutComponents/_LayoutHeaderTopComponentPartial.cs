using Microsoft.AspNetCore.Mvc;

namespace Limupa.UI.ViewComponents.LayoutComponents
{
    public class _LayoutHeaderTopComponentPartial:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
