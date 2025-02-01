using Microsoft.AspNetCore.Mvc;

namespace Limupa.UI.ViewComponents.ProductListComponents.ProductGameConsoleArmListComponents
{
    public class ProductXboxGameConsoleArmListComponentPartial:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
