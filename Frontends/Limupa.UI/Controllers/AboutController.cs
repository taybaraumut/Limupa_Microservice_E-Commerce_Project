using Limupa.UI.Services.CatalogServices.AboutServices;
using Microsoft.AspNetCore.Mvc;

namespace Limupa.UI.Controllers
{
    public class AboutController : Controller
    {
        private readonly IAboutService aboutService;

        public AboutController(IAboutService aboutService)
        {
            this.aboutService = aboutService;
        }

        public async Task<IActionResult> Index()
        {
            var values = await aboutService.GetAllAboutAsync();
            return View(values);
        }
    }
}
