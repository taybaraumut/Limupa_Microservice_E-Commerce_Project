using Limupa.Catalog.Dtos.CategoryDtos;
using Limupa.Catalog.Services.CategoryServices;
using Microsoft.AspNetCore.Mvc;

namespace Limupa.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> CategoryList()
        {
            var values = await categoryService.GetAllCategoryAsync();
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            await categoryService.CreateCategoryAsync(createCategoryDto);
            return Ok("Successful");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            await categoryService.DeleteCategoryAsync(id);
            return Ok("Successful");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            await categoryService.UpdateCategoryAsync(updateCategoryDto);
            return Ok("Successful");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(string id)
        {
            var value = await categoryService.GetByIdCategoryAsync(id);
            return Ok(value);
        }
    }
}
