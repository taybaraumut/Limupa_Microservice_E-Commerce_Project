using Microsoft.AspNetCore.Mvc;

namespace Limupa.UI.ViewComponents.ProductListComponents.ProductXiaomiPhoneComponents
{
    public class ProductXiaomiPhonePaginationComponentPartial : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
