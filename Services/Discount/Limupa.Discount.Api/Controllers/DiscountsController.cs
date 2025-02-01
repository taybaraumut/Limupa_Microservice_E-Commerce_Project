using Limupa.Discount.Dtos.DiscountCouponDtos;
using Limupa.Discount.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Limupa.Discount.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : ControllerBase
    {
        private readonly IDiscountCouponService discountCouponService;

        public DiscountsController(IDiscountCouponService discountCouponService)
        {
            this.discountCouponService = discountCouponService;
        }
        [HttpGet]
        public async Task<IActionResult> DiscountCouponList()
        {
            var values = await discountCouponService.GetAllDiscountCouponAsync();
            return Ok(values);
        }
        [HttpGet("GetCodeDetailByCode/{code}")]
        public async Task<IActionResult> GetCodeDetailByCode(string code)
        {
            var value = await discountCouponService.GetCodeDetailByCodeAsync(code);
            return Ok(value);
        }
        [HttpGet("GetDiscountCouponCountRate/{code}")]
        public IActionResult GetDiscountCouponCountRate(string code)
        {
            var value = discountCouponService.GetDiscountCouponCountRateAsync(code);
            return Ok(value);
        }
        [HttpPost]
        public async Task<IActionResult> CreateDiscountCoupon(CreateDiscountCouponDto createDiscountCouponDto)
        {
            await discountCouponService.CreateDiscountCouponAsync(createDiscountCouponDto);
            return Ok("Successful");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateDiscountCoupon(UpdateDiscountCouponDto updateDiscountCouponDto)
        {
            await discountCouponService.UpdateDiscountCouponAsync(updateDiscountCouponDto);
            return Ok("Successful");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscountCoupon(int id)
        {
            await discountCouponService.DeleteDiscountCouponAsync(id);
            return Ok("Successful");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDiscountCouponById(int id)
        {
            var values = await discountCouponService.GetByIdDiscountCouponAsync(id);
            return Ok(values);
        }
    }
}
