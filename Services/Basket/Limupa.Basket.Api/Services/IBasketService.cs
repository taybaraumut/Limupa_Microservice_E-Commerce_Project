using Limupa.Basket.Api.Dtos;

namespace Limupa.Basket.Api.Services
{
    public interface IBasketService
    {
        Task<BasketTotalDto> GetBasket(string userID);
        Task SaveBasket(BasketTotalDto basketTotalDto);
        Task DeleteBasket(string userID);
    }
}
