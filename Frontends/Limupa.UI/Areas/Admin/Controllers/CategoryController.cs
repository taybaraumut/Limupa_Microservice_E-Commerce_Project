using Limupa.DtoLayer.CategoryDtos;
using Limupa.UI.Services.CatalogServices.CategoryServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Limupa.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var values = await categoryService.GetAllCategoryAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            await categoryService.CreateCategoryAsync(createCategoryDto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCategory(string id)
        {
            var values = await categoryService.GetByIdCategoryAsync(id);
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            await categoryService.UpdateCategoryAsync(updateCategoryDto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            await categoryService.DeleteCategoryAsync(id);
            return RedirectToAction("Index");
        }
    }
}
