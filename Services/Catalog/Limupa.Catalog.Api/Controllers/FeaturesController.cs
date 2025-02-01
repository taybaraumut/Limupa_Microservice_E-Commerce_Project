using Limupa.Catalog.Api.Dtos.FeatureDtos;
using Limupa.Catalog.Api.Services.FeatureServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Limupa.Catalog.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly IFeatureService featureService;

        public FeaturesController(IFeatureService featureService)
        {
            this.featureService = featureService;
        }
        [HttpGet]
        public async Task<IActionResult> FeatureList()
        {
            var values = await featureService.GetAllFeatureAsync();
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateFeature(CreateFeatureDto createFeatureDto)
        {
            await featureService.CreateFeatureAsync(createFeatureDto);
            return Ok("Successful");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeature(string id)
        {
            await featureService.DeleteFeatureAsync(id);
            return Ok("Successful");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateFeature(UpdateFeatureDto updateFeatureDto)
        {
            await featureService.UpdateFeatureAsync(updateFeatureDto);
            return Ok("Successful");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeatureById(string id)
        {
            var value = await featureService.GetByIdFeatureAsync(id);
            return Ok(value);
        }
    }
}
