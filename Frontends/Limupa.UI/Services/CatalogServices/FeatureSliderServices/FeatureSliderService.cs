using Limupa.DtoLayer.FeatureSliderDtos;
using System.Collections.Generic;

namespace Limupa.UI.Services.CatalogServices.FeatureSliderServices
{
    public class FeatureSliderService : IFeatureSliderService
    {
        private readonly HttpClient httpClient;

        public FeatureSliderService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task CreateFeatureSliderAsync(CreateFeatureSliderDto createFeatureSliderDto)
        {
            await httpClient.PostAsJsonAsync("featuresliders", createFeatureSliderDto);
        }

        public async Task DeleteFeatureSliderAsync(string id)
        {
            await httpClient.DeleteAsync("featuresliders/" + id);
        }

        public Task FeatureSliderChangeStatusToFalse(string id)
        {
            throw new NotImplementedException();
        }

        public Task FeatureSliderChangeStatusToTrue(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ResultFeatureSliderDto>> GetAllFeatureSliderAsync()
        {
            var responseMessage = await httpClient.GetAsync("featuresliders");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultFeatureSliderDto>>();
            return values;
        }

        public async Task<GetByIdFeatureSliderDto> GetByIdFeatureSliderAsync(string id)
        {
            var responseMessage = await httpClient.GetAsync("featuresliders");
            var value = await responseMessage.Content.ReadFromJsonAsync<GetByIdFeatureSliderDto>();
            return value;
        }

        public async Task UpdateFeatureSliderAsync(UpdateFeatureSliderDto updateFeatureSliderDto)
        {
            await httpClient.PutAsJsonAsync("featuresliders", updateFeatureSliderDto);
        }
    }
}
