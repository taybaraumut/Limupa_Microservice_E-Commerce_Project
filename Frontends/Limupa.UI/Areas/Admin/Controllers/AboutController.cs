using IdentityModel;
using Limupa.DtoLayer.AboutDtos;
using Limupa.UI.Services.CatalogServices.AboutServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace Limupa.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutController : Controller
    {
        private readonly IAboutService aboutService;

        public AboutController(IAboutService aboutService)
        {
            this.aboutService = aboutService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var values = await aboutService.GetAllAboutAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateAbout()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAbout(CreateAboutDto createAboutDto)
        {
            await aboutService.CreateAboutAsync(createAboutDto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAbout(string id)
        {
            var values = await aboutService.GetByIdAboutAsync(id);
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDto updateAboutDto)
        {
            await aboutService.UpdateAboutAsync(updateAboutDto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAbout(string id)
        {
            await aboutService.DeleteAboutAsync(id);
            return RedirectToAction("Index");
        }
    }
}
