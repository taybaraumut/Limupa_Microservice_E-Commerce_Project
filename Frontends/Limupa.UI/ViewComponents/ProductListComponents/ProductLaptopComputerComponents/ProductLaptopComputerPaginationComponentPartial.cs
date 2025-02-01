using Microsoft.AspNetCore.Mvc;

namespace Limupa.UI.ViewComponents.ProductListComponents.ProductLaptopComputerComponents
{
    public class ProductLaptopComputerPaginationComponentPartial : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
