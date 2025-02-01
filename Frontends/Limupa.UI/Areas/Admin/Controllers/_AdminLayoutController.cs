using Microsoft.AspNetCore.Mvc;

namespace Limupa.UI.Areas.Admin.Controllers
{
    public class _AdminLayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
