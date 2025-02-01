using Limupa.DtoLayer.DiscountDtos;

namespace Limupa.UI.Services.DiscountServices
{
    public interface IDiscountService
    {
        Task<GetDiscountCodeDetailByCode> GetDiscountCode(string code);
        Task<int> GetDiscountCouponCountRateAsync(string code);
    }
}
