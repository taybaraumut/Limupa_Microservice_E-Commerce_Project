using Limupa.Basket.Api.Dtos;
using Limupa.Basket.Api.LoginServices;
using Limupa.Basket.Api.Services;
using Limupa.Basket.Api.Services.GoogleCloudStorageServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;


namespace Limupa.Basket.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketService basketService;
        private readonly ILoginService loginService;
        private readonly ICloudStorageService cloudStorageService;

        public BasketsController(IBasketService basketService,ILoginService loginService, ICloudStorageService cloudStorageService)
        {
            this.basketService = basketService;
            this.loginService = loginService;
            this.cloudStorageService = cloudStorageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasketDetail()
        {
            var values = await basketService.GetBasket(loginService.GetUserId);

            if(values != null)
            {
                foreach (var referee in values.BasketItems)
                {
                    await GenerateSignedUrl(referee);
                }
            }
            
            return Ok(values);
        }

        private async Task GenerateSignedUrl(BasketItemDto basketItemDto)
        {
            // Get Signed URL only when Saved File Name is available.
            if (!string.IsNullOrWhiteSpace(basketItemDto.SavedFileName))
            {
                basketItemDto.ProductImageUrl = await cloudStorageService.GetSignedUrlProductImageAsync(basketItemDto.SavedFileName);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveMyBasket(BasketTotalDto basketTotalDto)
        {
            basketTotalDto.UserID = loginService.GetUserId;
            await basketService.SaveBasket(basketTotalDto);

            return Ok("Successful");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBasket()
        {
            await basketService.DeleteBasket(loginService.GetUserId);
            return Ok("Successful");
        }
    }
}
