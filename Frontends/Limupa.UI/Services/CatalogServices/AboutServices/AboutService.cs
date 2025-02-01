using Limupa.DtoLayer.AboutDtos;

namespace Limupa.UI.Services.CatalogServices.AboutServices
{
    public class AboutService : IAboutService
    {
        private readonly HttpClient httpClient;

        public AboutService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task CreateAboutAsync(CreateAboutDto createAboutDto)
        {
            await httpClient.PostAsJsonAsync("abouts", createAboutDto);
        }

        public async Task DeleteAboutAsync(string id)
        {
            await httpClient.DeleteAsync("abouts/" + id);
        }

        public async Task<List<ResultAboutDto>> GetAllAboutAsync()
        {
            var responseMessage = await httpClient.GetAsync("abouts");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultAboutDto>>();
            return values;
        }

        public async Task<GetByIdAboutDto> GetByIdAboutAsync(string id)
        {
            var responseMessage = await httpClient.GetAsync("abouts/" + id);
            var value = await responseMessage.Content.ReadFromJsonAsync<GetByIdAboutDto>();
            return value;
        }

        public async Task UpdateAboutAsync(UpdateAboutDto updateAboutDto)
        {
            await httpClient.PutAsJsonAsync("abouts", updateAboutDto);
        }
    }
}
