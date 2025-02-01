using Limupa.DtoLayer.SpecialOfferDtos;

namespace Limupa.UI.Services.CatalogServices.SpecialOfferServices
{
    public class SpecialOfferService : ISpecialOfferService
    {
        private readonly HttpClient httpClient;

        public SpecialOfferService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task CreateSpecialOfferAsync(CreateSpecialOfferDto createSpecialOfferDto)
        {
            await httpClient.PostAsJsonAsync("specialoffers", createSpecialOfferDto);
        }

        public async Task DeleteSpecialOfferAsync(string id)
        {
            await httpClient.DeleteAsync("specialoffers/"+id);
        }

        public async Task<List<ResultSpecialOfferDto>> GetAllSpecialOfferAsync()
        {
            var responseMessage = await httpClient.GetAsync("specialoffers");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultSpecialOfferDto>>();
            return values;
        }

        public async Task<GetByIdSpecialOfferDto> GetByIdSpecialOfferAsync(string id)
        {
            var responseMessage = await httpClient.GetAsync("specialoffers/"+id);
            var value = await responseMessage.Content.ReadFromJsonAsync<GetByIdSpecialOfferDto>();
            return value;
        }

        public async Task UpdateSpecialOfferAsync(UpdateSpecialOfferDto updateSpecialOfferDto)
        {
            await httpClient.PostAsJsonAsync("specialoffers", updateSpecialOfferDto);
        }
    }
}
