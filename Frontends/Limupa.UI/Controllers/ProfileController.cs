using Limupa.UI.Services.Interfaces;
using Limupa.UI.Services.OrderServices.OrderOrderingServices;
using Microsoft.AspNetCore.Mvc;

namespace Limupa.UI.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IOrderOrderingService orderOrderingService;
        private readonly IUserService userService;

        public ProfileController(IOrderOrderingService orderOrderingService, IUserService userService)
        {
            this.orderOrderingService = orderOrderingService;
            this.userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var value = await userService.GetUserInfo();
            TempData["name"] = value.Name;
            TempData["surname"] = value.Surname;
            TempData["username"] = value.Username;
            TempData["email"] = value.Email;
            
            var values = await orderOrderingService.GetOrderingByUserId(value.Id);
            return View(values);
        }
    }
}
