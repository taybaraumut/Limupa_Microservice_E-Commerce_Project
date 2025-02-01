using Limupa.Catalog.Api.Dtos.AboutDtos;
using Limupa.Catalog.Api.Services.AboutServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Limupa.Catalog.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AboutsController : ControllerBase
    {
        private readonly IAboutService aboutService;

        public AboutsController(IAboutService aboutService)
        {
            this.aboutService = aboutService;
        }
        [HttpGet]
        public async Task<IActionResult> AboutList()
        {
            var values = await aboutService.GetAllAboutAsync();
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAbout(CreateAboutDto createAboutDto)
        {
            await aboutService.CreateAboutAsync(createAboutDto);
            return Ok("Successful");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAbout(string id)
        {
            await aboutService.DeleteAboutAsync(id);
            return Ok("Successful");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDto updateAboutDto)
        {
            await aboutService.UpdateAboutAsync(updateAboutDto);
            return Ok("Successful");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAboutById(string id)
        {
            var value = await aboutService.GetByIdAboutAsync(id);
            return Ok(value);
        }
    }
}
