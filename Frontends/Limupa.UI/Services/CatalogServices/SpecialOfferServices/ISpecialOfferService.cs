using Limupa.DtoLayer.SpecialOfferDtos;

namespace Limupa.UI.Services.CatalogServices.SpecialOfferServices
{
    public interface ISpecialOfferService
    {
        Task<List<ResultSpecialOfferDto>> GetAllSpecialOfferAsync();
        Task CreateSpecialOfferAsync(CreateSpecialOfferDto createSpecialOfferDto);
        Task UpdateSpecialOfferAsync(UpdateSpecialOfferDto updateSpecialOfferDto);
        Task<GetByIdSpecialOfferDto> GetByIdSpecialOfferAsync(string id);
        Task DeleteSpecialOfferAsync(string id);
    }
}
