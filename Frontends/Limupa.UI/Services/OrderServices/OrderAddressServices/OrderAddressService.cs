using Limupa.DtoLayer.OrderDtos.OrderAddressDtos;

namespace Limupa.UI.Services.OrderServices.OrderAddressServices
{
    public class OrderAddressService : IOrderAddressService
    {
        private readonly HttpClient httpClient;

        public OrderAddressService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task CreateOrderAddressAsync(CreateOrderAddressDto createOrderAddressDto)
        {
            var value = await httpClient.PostAsJsonAsync("addresses", createOrderAddressDto);
            if (value.IsSuccessStatusCode)
            {
                Console.WriteLine("");
            }
        }
    }
}
