using Limupa.DtoLayer.FeatureDtos;

namespace Limupa.UI.Services.CatalogServices.FeatureServices
{
    public interface IFeatureService
    {
        Task<List<ResultFeatureDto>> GetAllFeatureAsync();
        Task CreateFeatureAsync(CreateFeatureDto createCategoryDto);
        Task UpdateFeatureAsync(UpdateFeatureDto updateCategoryDto);
        Task<GetByIdFeatureDto> GetByIdFeatureAsync(string id);
        Task DeleteFeatureAsync(string id);
    }
}
