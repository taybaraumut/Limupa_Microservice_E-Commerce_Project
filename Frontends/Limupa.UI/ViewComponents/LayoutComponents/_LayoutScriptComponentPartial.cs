using Microsoft.AspNetCore.Mvc;

namespace Limupa.UI.ViewComponents.LayoutComponents
{
    public class _LayoutScriptComponentPartial:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
