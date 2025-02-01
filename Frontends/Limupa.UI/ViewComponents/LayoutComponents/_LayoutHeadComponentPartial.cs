using Microsoft.AspNetCore.Mvc;

namespace Limupa.UI.ViewComponents.LayoutComponents
{
    public class _LayoutHeadComponentPartial:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
