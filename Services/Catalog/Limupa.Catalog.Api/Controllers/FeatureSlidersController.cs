using Limupa.Catalog.Api.Dtos.FeatureSliderDtos;
using Limupa.Catalog.Api.Services.FeatureSliderServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Limupa.Catalog.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureSlidersController : ControllerBase
    {
        private readonly IFeatureSliderService featureSliderService;

        public FeatureSlidersController(IFeatureSliderService featureSliderService)
        {
            this.featureSliderService = featureSliderService;
        }
        [HttpGet]
        public async Task<IActionResult> FeatureSliderList()
        {
            var values = await featureSliderService.GetAllFeatureSliderAsync();
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateFeatureSlider(CreateFeatureSliderDto createFeatureSliderDto)
        {
            await featureSliderService.CreateFeatureSliderAsync(createFeatureSliderDto);
            return Ok("Successful");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeatureSlider(string id)
        {
            await featureSliderService.DeleteFeatureSliderAsync(id);
            return Ok("Successful");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateFeatureSlider(UpdateFeatureSliderDto updateFeatureSliderDto)
        {
            await featureSliderService.UpdateFeatureSliderAsync(updateFeatureSliderDto);
            return Ok("Successful");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeatureSliderById(string id)
        {
            var value = await featureSliderService.GetByIdFeatureSliderAsync(id);
            return Ok(value);
        }
    }
}
