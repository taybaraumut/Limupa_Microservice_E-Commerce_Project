using Limupa.Catalog.Dtos.ProductDetailDtos;
using Limupa.Catalog.Services.ProductDetailServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Limupa.Catalog.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailsController : ControllerBase
    {
        private readonly IProductDetailService productDetailService;

        public ProductDetailsController(IProductDetailService productDetailService)
        {
            this.productDetailService = productDetailService;
        }
        [HttpGet]
        public async Task<IActionResult> ProductDetailList()
        {
            var values = await productDetailService.GetAllProductDetailAsync();
            return Ok(values);
        }
        [HttpGet("ProductDetailByProductId/{id}")]
        public async Task<IActionResult> ProductDetailByProductId(string id)
        {
            var value = await productDetailService.GetProductDetailByProductIdAsync(id);
            return Ok(value);
        }
        [HttpGet("ProductDetailWithProductList")]
        public async Task<IActionResult> ProductDetailWithProductList()
        {
            var values = await productDetailService.GetProductDetailWithProductAsync();
            return Ok(values);
        }       
        [HttpPost]
        public async Task<IActionResult> CreateProductDetail(CreateProductDetailDto createProductDetailDto)
        {
            await productDetailService.CreateProductDetailAsync(createProductDetailDto);
            return Ok("Successful");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductDetail(string id)
        {
            await productDetailService.DeleteProductDetailAsync(id);
            return Ok("Successful");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProductDetail(UpdateProductDetailDto updateProductDetailDto)
        {
            await productDetailService.UpdateProductDetailAsync(updateProductDetailDto);
            return Ok("Successful");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductDetailById(string id)
        {
            var value = await productDetailService.GetByIdProductDetailAsync(id);
            return Ok(value);
        }
    }
}
