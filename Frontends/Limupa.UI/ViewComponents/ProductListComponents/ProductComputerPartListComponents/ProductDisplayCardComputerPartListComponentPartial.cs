using Microsoft.AspNetCore.Mvc;

namespace Limupa.UI.ViewComponents.ProductListComponents.ProductComputerPartListComponents
{
    public class ProductDisplayCardComputerPartListComponentPartial : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
