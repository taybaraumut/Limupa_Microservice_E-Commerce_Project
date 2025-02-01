using Limupa.DtoLayer.AccountDtos;
using Limupa.UI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Limupa.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {       
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }      
    }
}
