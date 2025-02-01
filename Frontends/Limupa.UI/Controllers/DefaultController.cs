using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Runtime.InteropServices.JavaScript;

namespace Limupa.UI.Controllers
{
    public class DefaultController : Controller
    {
        public async Task<IActionResult> Index()
        {           
            return View();
        }
    }
}
