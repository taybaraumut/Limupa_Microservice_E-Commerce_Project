using Limupa.DtoLayer.FeatureDtos;

namespace Limupa.UI.Services.CatalogServices.FeatureServices
{
    public class FeatureService : IFeatureService
    {
        private readonly HttpClient httpClient;

        public FeatureService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task CreateFeatureAsync(CreateFeatureDto createFeatureDto)
        {
            await httpClient.PostAsJsonAsync("features", createFeatureDto);
        }

        public async Task DeleteFeatureAsync(string id)
        {
            await httpClient.DeleteAsync("features/" + id);
        }

        public async Task<List<ResultFeatureDto>> GetAllFeatureAsync()
        {
            var responseMessage = await httpClient.GetAsync("features");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultFeatureDto>>();
            return values;
        }

        public async Task<GetByIdFeatureDto> GetByIdFeatureAsync(string id)
        {
            var responseMessage = await httpClient.GetAsync("features");
            var value = await responseMessage.Content.ReadFromJsonAsync<GetByIdFeatureDto>();
            return value;
        }

        public async Task UpdateFeatureAsync(UpdateFeatureDto updateFeatureDto)
        {
            await httpClient.PutAsJsonAsync("features", updateFeatureDto);
        }
    }
}
