using Microsoft.AspNetCore.Mvc;

namespace Limupa.UI.ViewComponents.ProductDetailComponents
{
    public class ProductDetailProductListComponentPartial : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
