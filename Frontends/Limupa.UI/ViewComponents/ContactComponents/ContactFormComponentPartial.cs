using Microsoft.AspNetCore.Mvc;

namespace Limupa.UI.ViewComponents.ContactComponents
{
    public class ContactFormComponentPartial:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
