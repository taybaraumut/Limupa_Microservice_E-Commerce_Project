using Limupa.DtoLayer.AboutDtos;

namespace Limupa.UI.Services.CatalogServices.AboutServices
{
    public interface IAboutService
    {
        Task<List<ResultAboutDto>> GetAllAboutAsync();
        Task CreateAboutAsync(CreateAboutDto createAboutDto);
        Task UpdateAboutAsync(UpdateAboutDto updateAboutDto);
        Task<GetByIdAboutDto> GetByIdAboutAsync(string id);
        Task DeleteAboutAsync(string id);
    }
}
