using Microsoft.AspNetCore.Mvc;

namespace Limupa.UI.ViewComponents.DefaultComponents
{
    public class DefaultGeneralProductComponentPartial:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
