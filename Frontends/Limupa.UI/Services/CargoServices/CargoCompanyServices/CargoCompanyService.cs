using Limupa.DtoLayer.CargoDtos.CargoCompanyDtos;

namespace Limupa.UI.Services.CargoServices.CargoCompanyServices
{
    public class CargoCompanyService : ICargoCompanyService
    {
        private readonly HttpClient httpClient;

        public CargoCompanyService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task CreateCargoCompanyAsync(CreateCargoCompanyDto createCargoCompanyDto)
        {
            await httpClient.PostAsJsonAsync("cargocompanies", createCargoCompanyDto);
        }

        public async Task DeleteCargoCompanyAsync(string id)
        {
            await httpClient.DeleteAsync("cargocompanies/" + id);
        }

        public async Task<List<ResultCargoCompanyDto>> GetAllCargoCompanyAsync()
        {
            var responseMessage = await httpClient.GetAsync("cargocompanies");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultCargoCompanyDto>>();
            return values;
        }

        public async Task<GetByIdCargoCompanyDto> GetByIdCargoCompanyAsync(string id)
        {
            var responseMessage = await httpClient.GetAsync("cargocompanies/" + id);
            var value = await responseMessage.Content.ReadFromJsonAsync<GetByIdCargoCompanyDto>();
            return value;
        }

        public async Task UpdateCargoCompanyAsync(UpdateCargoCompanyDto updateCargoCompanyDto)
        {
            await httpClient.PutAsJsonAsync("cargocompanies", updateCargoCompanyDto);
        }
    }
}