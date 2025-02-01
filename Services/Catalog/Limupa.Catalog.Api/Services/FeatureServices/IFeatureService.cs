using Limupa.Catalog.Api.Dtos.FeatureDtos;

namespace Limupa.Catalog.Api.Services.FeatureServices
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
