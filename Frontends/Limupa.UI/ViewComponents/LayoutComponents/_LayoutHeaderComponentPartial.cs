using Limupa.DtoLayer.CategoryDtos;
using Limupa.UI.Services.CatalogServices.CategoryServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Limupa.UI.ViewComponents.LayoutComponents
{
    public class _LayoutHeaderComponentPartial:ViewComponent
    {
        private readonly ICategoryService categoryService;

        public _LayoutHeaderComponentPartial(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await categoryService.GetAllCategoryAsync();
            return View(values);
        }
    }
}
