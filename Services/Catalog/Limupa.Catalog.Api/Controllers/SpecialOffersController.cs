using Limupa.Catalog.Api.Dtos.SpecialOfferDtos;
using Limupa.Catalog.Api.Services.SpecialOfferServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Limupa.Catalog.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialOffersController : ControllerBase
    {
        private readonly ISpecialOfferService specialOfferService;

        public SpecialOffersController(ISpecialOfferService specialOfferService)
        {
            this.specialOfferService = specialOfferService;
        }
        [HttpGet]
        public async Task<IActionResult> SpecialOfferList()
        {
            var values = await specialOfferService.GetAllSpecialOfferAsync();
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateSpecialOffer(CreateSpecialOfferDto createSpecialOfferDto)
        {
            await specialOfferService.CreateSpecialOfferAsync(createSpecialOfferDto);
            return Ok("Successful");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpecialOffer(string id)
        {
            await specialOfferService.DeleteSpecialOfferAsync(id);
            return Ok("Successful");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateSpecialOffer(UpdateSpecialOfferDto updateSpecialOfferDto)
        {
            await specialOfferService.UpdateSpecialOfferAsync(updateSpecialOfferDto);
            return Ok("Successful");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSpecialOfferById(string id)
        {
            var value = await specialOfferService.GetByIdSpecialOfferAsync(id);
            return Ok(value);
        }
    }
}
