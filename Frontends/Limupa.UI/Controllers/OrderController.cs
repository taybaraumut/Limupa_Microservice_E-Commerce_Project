using Limupa.DtoLayer.OrderDtos.OrderAddressDtos;
using Limupa.UI.Services.Interfaces;
using Limupa.UI.Services.OrderServices.OrderAddressServices;
using Microsoft.AspNetCore.Mvc;

namespace Limupa.UI.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderAddressService orderAddressService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public OrderController(IOrderAddressService orderAddressService, IHttpContextAccessor httpContextAccessor)
        {
            this.orderAddressService = orderAddressService;
            this.httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user_id = httpContextAccessor?.HttpContext?.User?.FindFirst("sub")?.Value;
            if (user_id != null)
            {
                return View();
            }
            return RedirectToAction("Index","Default");
        }
        [HttpPost]
        public async Task<IActionResult> Index(CreateOrderAddressDto createOrderAddressDto)
        {
            createOrderAddressDto.Description = "ddd";
            await orderAddressService.CreateOrderAddressAsync(createOrderAddressDto);
            return RedirectToAction("Index");
        }
    }
}
