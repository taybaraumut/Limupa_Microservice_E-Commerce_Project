using Limupa.Basket.Api.Dtos;
using Limupa.Basket.Api.Settings;
using System.Text.Json;

namespace Limupa.Basket.Api.Services
{
    public class BasketService : IBasketService
    {
        private readonly RedisService redisService;

        public BasketService(RedisService redisService)
        {
            this.redisService = redisService;
        }

        public async Task DeleteBasket(string userID)
        {
            await redisService.GetDb().KeyDeleteAsync(userID);
        }

        public async Task<BasketTotalDto> GetBasket(string userID)
        {
            var existBasket = await redisService.GetDb().StringGetAsync(userID);           

            if (existBasket.IsNullOrEmpty)
            {
                return null;
            }
            return JsonSerializer.Deserialize<BasketTotalDto>(existBasket!)!;
        }

        public async Task SaveBasket(BasketTotalDto basketTotalDto)
        {
            await redisService.GetDb().StringSetAsync(basketTotalDto.UserID,JsonSerializer.Serialize(basketTotalDto));
        }
    }
}
