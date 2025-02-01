using Microsoft.AspNetCore.Mvc;

namespace Limupa.UI.ViewComponents.ProductListComponents.ProductApplePhoneComponents
{
    public class ProductApplePhonePaginationComponentPartial:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
