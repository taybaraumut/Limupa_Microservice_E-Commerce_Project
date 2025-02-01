using Limupa.DtoLayer.BasketDtos;
using System.Net;

namespace Limupa.UI.Services.BasketServices
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient httpClient;
        private readonly IHttpContextAccessor httpContextAccessor;
        public BasketService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            this.httpClient = httpClient;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task AddBasketItem(BasketItemDto basketItemDto)
        {
            // Sepeti al
            var values = await GetBasket();

            // Sepet yoksa yeni bir sepet oluştur
            if (values == null)
            {
                values = new BasketTotalDto();
                values.BasketItems = new List<BasketItemDto>();
            }

            // Sepette ürün olup olmadığını kontrol et ve ekle
            if (!values.BasketItems.Any(x => x.ProductUrlSeo == basketItemDto.ProductUrlSeo))
            {
                values.BasketItems.Add(basketItemDto);
            }

            values.UserID = httpContextAccessor.HttpContext!.User.FindFirst("sub")!.Value;
            values.DiscountRate = 0;
            values.DiscountCode = "Yok";
            // Sepeti kaydet
            await SaveBasket(values);
        }

        public async Task DeleteBasket(string userID)
        {
            throw new NotImplementedException();
        }

        public async Task<BasketTotalDto> GetBasket()
        {
            var responseMessage = await httpClient.GetAsync("baskets");
            if (responseMessage.StatusCode == HttpStatusCode.NoContent)
            {
                return null; // Eğer yanıt NoContent ise veya istek başarısızsa, null döndür
            }
            var values = await responseMessage.Content.ReadFromJsonAsync<BasketTotalDto>();
            return values;
        }

        public async Task<bool> RemoveBasketItem(string productId)
        {
            var values = await GetBasket();
            var deletedItem = values.BasketItems.FirstOrDefault(x => x.ProductUrlSeo == productId);
            var result = values.BasketItems.Remove(deletedItem!);
            await SaveBasket(values);
            return true;
        }

        public async Task SaveBasket(BasketTotalDto basketTotalDto)
        {
            await httpClient.PostAsJsonAsync<BasketTotalDto>("baskets", basketTotalDto);
        }
    }
}
