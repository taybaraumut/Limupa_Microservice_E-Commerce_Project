using Limupa.DtoLayer.DiscountDtos;

namespace Limupa.UI.Services.DiscountServices
{
    public class DiscountService : IDiscountService
    {
        private readonly HttpClient httpClient;

        public DiscountService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<GetDiscountCodeDetailByCode> GetDiscountCode(string code)
        {
            var responseMessage = await httpClient.GetAsync($"discounts/GetCodeDetailByCode/{code}");
            var values = await responseMessage.Content.ReadFromJsonAsync<GetDiscountCodeDetailByCode>();
            return values;
        }

        public async Task<int> GetDiscountCouponCountRateAsync(string code)
        {
            var responseMessage = await httpClient.GetAsync($"discounts/GetDiscountCouponCountRate/{code}");
            var values = await responseMessage.Content.ReadFromJsonAsync<int>();
            return values;
        }
    }
}
