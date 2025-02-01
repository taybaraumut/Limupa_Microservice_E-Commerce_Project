using Google.Apis.Storage.v1;
using Google.Cloud.Storage.V1;
using Limupa.Catalog.Api.Dtos.ProductImageDtos;
using Limupa.Catalog.Api.Services.GoogleCloudStorageServices;
using Limupa.Catalog.Dtos.ProductImageDtos;
using Limupa.Catalog.Entities;
using Limupa.Catalog.Services.ProductImageServices;
using Limupa.Catalog.Services.ProductServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using System.Security.Cryptography.Xml;
using static System.Net.Mime.MediaTypeNames;

namespace Limupa.Catalog.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImagesController : ControllerBase
    {
        private readonly IProductImageService productImageService;
        private readonly ICloudStorageService cloudStorageService;

        public ProductImagesController(IProductImageService productImageService,ICloudStorageService cloudStorageService)
        {
            this.productImageService = productImageService;
            this.cloudStorageService = cloudStorageService;
        }
        [HttpGet]
        public async Task<IActionResult> ProductImageList()
        {
            var values = await productImageService.GetAllProductImageAsync();
            return Ok(values);
        }
        [HttpGet("ProductImageWithProductList")]
        public async Task<IActionResult> ProductImageWithProductList()
        {
            var values = await productImageService.GetProductImageWithProductAsync();
            return Ok(values);
        }
        [HttpGet("ProductImageByProductId/{id}")]
        public async Task<IActionResult> ProductImageByProductId(string id)
        {
            var values = await productImageService.GetProductImageByProductIdAsync(id);

            if (values != null)
            {
                await cloudStorageService.GenerateSignedUrlProductImageAsync(values);
            }

            return Ok(values);
        }

        //private async Task GenerateSignedUrls(GetProductImageByProductIdDto product)
        //{
        //    await cloudStorageService.GenerateSignedUrlProductImageAsync(product);
        //}

        [HttpGet("ProductImageByProductIdCheck/{id}")]
        public async Task<IActionResult> ProductImageByProductIdCheck(string id)
        {
            var value = await productImageService.GetProductImageByProductIdCheckAsync(id);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductImage(CreateProductImageDto createProductImageDto)
        {
            if (ModelState.IsValid)
            {
                await cloudStorageService.GoogleStorageProductImageAdditionControlAsync(createProductImageDto);
                await productImageService.CreateProductImageAsync(createProductImageDto);
            }
           
            return Ok("Success");
        }
        //private string? GenerateFileNameToSave(string incomingFileName)
        //{
        //    var fileName = Path.GetFileNameWithoutExtension(incomingFileName);
        //    var extension = Path.GetExtension(incomingFileName);
        //    return $"{fileName}-{DateTime.Now.ToUniversalTime().ToString("yyyyMMddHHmmss")}{extension}";
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductImage(string id)
        {
            await productImageService.DeleteProductImageAsync(id);
            return Ok("Successful");
        }


        [HttpPut]
        public async Task<IActionResult> UpdateProductImage(UpdateProductImageDto updateProductImageDto)
        {
            await cloudStorageService.ReplaceProductImagePhoto(updateProductImageDto);
            await productImageService.UpdateProductImageAsync(updateProductImageDto);
            return Ok("Success");
        }

        //private async Task ReplacePhoto(UpdateProductImageDto updateProductImageDto)
        //{
            
        //}



        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductImageById(string id)
        {
            var value = await productImageService.GetByIdProductImageAsync(id);
            return Ok(value);
        }
    }
}
