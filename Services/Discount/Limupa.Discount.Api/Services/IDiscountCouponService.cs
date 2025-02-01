using Limupa.Discount.Api.Dtos.DiscountCouponDtos;
using Limupa.Discount.Dtos.DiscountCouponDtos;

namespace Limupa.Discount.Services
{
    public interface IDiscountCouponService
    {
        Task<List<ResultDiscountCouponDto>> GetAllDiscountCouponAsync();
        Task CreateDiscountCouponAsync(CreateDiscountCouponDto createDiscountCouponDto);
        Task UpdateDiscountCouponAsync(UpdateDiscountCouponDto updateDiscountCouponDto);
        Task DeleteDiscountCouponAsync(int id);
        Task<GetByIdDiscountCouponDto> GetByIdDiscountCouponAsync(int id);
        Task<GetDiscountCodeDetailByCode> GetCodeDetailByCodeAsync(string code);
        int GetDiscountCouponCountRateAsync(string code);
    }
}
