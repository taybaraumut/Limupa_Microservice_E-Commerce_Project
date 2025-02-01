using Limupa.UI.Services.CatalogServices.ProductServices;
using Microsoft.AspNetCore.Mvc;

namespace Limupa.UI.Controllers
{
    public class ProductSearchController : Controller
    {
        private readonly IProductService productService;

        public ProductSearchController(IProductService productService)
        {
            this.productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string productCategory, string productTextSearch)
        {
            var values = await productService.GetSearchProductAsync(productCategory,productTextSearch);
            TempData["searchCount"] = values.Count();
            return View(values);
        }
    }
}
