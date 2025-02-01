using Limupa.DtoLayer.CategoryDtos;

namespace Limupa.UI.Services.CatalogServices.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient httpClient;

        public CategoryService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            await httpClient.PostAsJsonAsync("categories", createCategoryDto);
        }

        public async Task DeleteCategoryAsync(string id)
        {
            await httpClient.DeleteAsync("categories/" + id);
        }

        public async Task<List<ResultCategoryDto>> GetAllCategoryAsync()
        {
            var responseMessage = await httpClient.GetAsync("categories");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultCategoryDto>>();
            return values;
        }

        public async Task<GetByIdCategoryDto> GetByIdCategoryAsync(string id)
        {
            var responseMessage = await httpClient.GetAsync("categories/" + id);
            var value = await responseMessage.Content.ReadFromJsonAsync<GetByIdCategoryDto>();
            return value;
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
        {
            await httpClient.PutAsJsonAsync("categories", updateCategoryDto);
        }
    }
}
