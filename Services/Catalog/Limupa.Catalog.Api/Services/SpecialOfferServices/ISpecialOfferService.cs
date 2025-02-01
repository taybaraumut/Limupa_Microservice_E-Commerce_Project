using Limupa.Catalog.Api.Dtos.SpecialOfferDtos;

namespace Limupa.Catalog.Api.Services.SpecialOfferServices
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
