using Limupa.DtoLayer.OrderDtos.OrderOrderingDtos;

namespace Limupa.UI.Services.OrderServices.OrderOrderingServices
{
    public class OrderOrderingService : IOrderOrderingService
    {
        private readonly HttpClient httpClient;

        public OrderOrderingService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<ResultOrderingByUserIdDto>> GetOrderingByUserId(string userId)
        {
            var responseMessage = await httpClient.GetAsync($"orderings/GetOrderingByUserId/{userId}");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultOrderingByUserIdDto>>();
            return values;
        }
    }
}
