using Limupa.DtoLayer.CategoryDtos;

namespace Limupa.UI.Services.CatalogServices.CategoryServices
{
    public interface ICategoryService
    {
        Task<List<ResultCategoryDto>> GetAllCategoryAsync();
        Task CreateCategoryAsync(CreateCategoryDto createCategoryDto);
        Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto);
        Task<GetByIdCategoryDto> GetByIdCategoryAsync(string id);
        Task DeleteCategoryAsync(string id);
    }
}
