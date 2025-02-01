using Limupa.Basket.Api.Services;
using Limupa.Catalog.Api.Dtos.ProductDtos;
using Limupa.Catalog.Api.Services.GoogleCloudStorageServices;
using Limupa.Catalog.Dtos.ProductDtos;
using Limupa.Catalog.Services.ProductServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Limupa.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly ICloudStorageService cloudStorageService;

        public ProductsController(IProductService productService, ICloudStorageService cloudStorageService)
        {
            this.productService = productService;
            this.cloudStorageService = cloudStorageService;
        }
        [HttpGet("ProductList")]
        public async Task<IActionResult> ProductList()
        {
            var values = await productService.GetAllProductAsync();
            return Ok(values);
        }
        [HttpGet("GetLastTenProduct")]
        public async Task<IActionResult> GetLastTenProduct()
        {
            var values = await productService.GetLastTenProductAsync();

            foreach (var referee in values)
            {
                await cloudStorageService.GenerateSignedUrlProductLastTenAsync(referee);
            }

            return Ok(values);
        }
        [HttpGet("GetTenDataByProductPersonelCareCategory")]
        public async Task<IActionResult> GetTenDataByProductPersonelCareCategory()
        {
            var values = await productService.GetTenDataByProductPersonelCareCategoryAsync();

            foreach (var referee in values)
            {
                await cloudStorageService.GenerateSignedUrlProductPersonelCareCategoryAsync(referee);
            }

            //values.Select(async x =>
            //{
            //    await cloudStorageService.GenerateSignedUrlProductPersonelCareCategoryAsync(x);

            //}).ToList();

            return Ok(values);
        }

        [HttpGet("GetProductByElectronicDeviceCategory")]
        public async Task<IActionResult> GetProductByElectronicDeviceCategory()
        {
            var values = await productService.GetProductByElectronicDeviceCategoryAsync();

            foreach (var referee in values)
            {
                await cloudStorageService.GenerateSignedUrlProductByElectronicDeviceCategoryAsync(referee);
            }

            return Ok(values);
        }

        [HttpGet("GetProductByHomeAndEntertainmentDeviceCategory")]
        public async Task<IActionResult> GetProductByHomeAndEntertainmentDeviceCategory()
        {
            var values = await productService.GetProductByHomeAndEntertainmentDeviceCategoryAsync();
            return Ok(values);
        }

        [HttpGet("GetProductİpohnePhoneModel")]
        public async Task<IActionResult> GetProductİpohnePhoneModel()
        {
            var values = await productService.GetProductİpohnePhoneModelAsync();

            foreach (var referee in values)
            {
                await cloudStorageService.GenerateSignedUrlProductİpohnePhoneModelAsync(referee);
            }

            return Ok(values);
        }

        [HttpGet("GetProductMackbookLaptopModel")]
        public async Task<IActionResult> GetProductMackbookLaptopModel()
        {
            var values = await productService.GetProductMackbookLaptopModelAsync();

            foreach (var referee in values)
            {
                await cloudStorageService.GenerateSignedUrlProductMackbookLaptopModelAsync(referee);
            }

            return Ok(values);
        }

        [HttpGet("GetProductVideoGameModel")]
        public async Task<IActionResult> GetProductVideoGameModel()
        {
            var values = await productService.GetProductVideoGameModelAsync();

            foreach (var referee in values)
            {
                await cloudStorageService.GenerateSignedUrlProductVideoGameModelAsync(referee);
            }

            return Ok(values);
        }
        [HttpGet("ProductBluetoothListFilter")]
        public async Task<IActionResult> ProductBluetoothListFilter([FromQuery] List<string> productName, [FromQuery] List<decimal> productPrice, [FromQuery] List<string> productModel)
        {
            var values = await productService.GetProductBluetoothListFilterAsync(productName, productPrice, productModel);

            foreach (var referee in values)
            {
                await cloudStorageService.GenerateSignedUrlProductBluetoothListAsync(referee);
            }

            return Ok(values);
        }
        [HttpGet("GetProductBluetoothList")]
        public async Task<IActionResult> GetProductBluetoothList()
        {
            var values = await productService.GetProductBluetoothListAsync();

            foreach (var referee in values)
            {
                await cloudStorageService.GenerateSignedUrlProductBluetoothListAsync(referee);
            }

            return Ok(values);
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetProductById(string id)
        //{
        //    var value = await productService.GetByIdProductAsync(id);

        //    await cloudStorageService.GenerateSignedUrlOne(value);

        //    return Ok(value);
        //}

        [HttpGet("ProductWithCategoryList")]
        public async Task<IActionResult> ProductWithCategoryList()
        {
            var values = await productService.GetProductWithCategoryAsync();

            foreach (var referee in values)
            {
                await cloudStorageService.GenerateSignedUrlTwo(referee);
            }
            return Ok(values);
        }
        [HttpGet("GetSearchProduct")]
        public async Task<IActionResult> GetSearchProduct(string productCategory,string productTextSearch)
        {
            var values = await productService.GetSearchProductAsync(productCategory,productTextSearch);

            foreach (var referee in values)
            {
                await cloudStorageService.GenerateSignedUrlTwo(referee);
            }
            
            return Ok(values);
        }
        [HttpGet("GetProductGeneralPhoneListFilter")]
        public async Task<IActionResult> GetProductGeneralPhoneListFilter([FromQuery] List<string> productName, [FromQuery] List<decimal> productPrice, [FromQuery] List<string> productModel, [FromQuery] List<string> productInternalMemorySize, [FromQuery] List<string> productMobileRamSize)
        {
            var values = await productService.GetProductGeneralPhoneListFilterAsync(productName, productPrice, productModel,productInternalMemorySize,productMobileRamSize);

            foreach (var referee in values)
            {
                await cloudStorageService.GenerateSignedUrlProductGeneralPhoneListAsync(referee);
            }

            return Ok(values);
        }
        [HttpGet("GetProductGeneralPhoneList")]
        public async Task<IActionResult> GetProductGeneralPhoneList()
        {
            var values = await productService.GetProductGeneralPhoneListAsync();

            foreach (var referee in values)
            {
                await cloudStorageService.GenerateSignedUrlProductGeneralPhoneListAsync(referee);
            }

            return Ok(values);
        }
        [HttpGet("GetProductİphonePhoneList")]
        public async Task<IActionResult> GetProductİphonePhoneList()
        {
            var values = await productService.GetProductİphonePhoneListAsync();

            foreach (var referee in values)
            {
                await cloudStorageService.GenerateSignedUrlProductİphonePhoneListAsync(referee);
            }

            return Ok(values);
        }
        [HttpGet("GetProductİphonePhoneListFilter")]
        public async Task<IActionResult> GetProductİphonePhoneListFilter([FromQuery] List<string> productName, [FromQuery] List<decimal> productPrice, [FromQuery] List<string> productModel, [FromQuery] List<string> productInternalMemorySize)
        {
            var values = await productService.GetProductİphonePhoneListFilterAsync(productName,productPrice,productModel,productInternalMemorySize);

            foreach (var referee in values)
            {
                await cloudStorageService.GenerateSignedUrlProductİphonePhoneListAsync(referee);
            }

            return Ok(values);
        }
        [HttpGet("GetProductSamsungPhoneListFilter")]
        public async Task<IActionResult> GetProductSamsungPhoneListFilter([FromQuery] List<string> productName, [FromQuery] List<decimal> productPrice, [FromQuery] List<string> productModel, [FromQuery] List<string> productInternalMemorySize, [FromQuery] List<string> productMobileRamSize)
        {
            var values = await productService.GetProductSamsungPhoneListFilterAsync(productName, productPrice, productModel, productInternalMemorySize, productMobileRamSize);

            foreach (var referee in values)
            {
                await cloudStorageService.GenerateSignedUrlProductSamsungPhoneListAsync(referee);
            }

            return Ok(values);
        }
        [HttpGet("GetProductSamsungPhoneList")]
        public async Task<IActionResult> GetProductSamsungPhoneList()
        {
            var values = await productService.GetProductSamsungPhoneListAsync();

            foreach (var referee in values)
            {
                await cloudStorageService.GenerateSignedUrlProductSamsungPhoneListAsync(referee);
            }

            return Ok(values);
        }
        [HttpGet("GetProductXiaomiPhoneListFilter")]
        public async Task<IActionResult> GetProductXiaomiPhoneListFilter([FromQuery] List<string> productName, [FromQuery] List<decimal> productPrice, [FromQuery] List<string> productModel, [FromQuery] List<string> productInternalMemorySize, [FromQuery] List<string> productMobileRamSize)
        {
            var values = await productService.GetProductXiaomiPhoneListFilterAsync(productName, productPrice, productModel, productInternalMemorySize, productMobileRamSize);

            foreach (var referee in values)
            {
                await cloudStorageService.GenerateSignedUrlProductXiaomiPhoneListAsync(referee);
            }

            return Ok(values);
        }
        [HttpGet("GetProductXiaomiPhoneList")]
        public async Task<IActionResult> GetProductXiaomiPhoneList()
        {
            var values = await productService.GetProductXiaomiPhoneListAsync();

            foreach (var referee in values)
            {
                await cloudStorageService.GenerateSignedUrlProductXiaomiPhoneListAsync(referee);
            }

            return Ok(values);
        }
        [HttpGet("GetProductRealmePhoneListFilter")]
        public async Task<IActionResult> GetProductRealmePhoneListFilter([FromQuery] List<string> productName, [FromQuery] List<decimal> productPrice, [FromQuery] List<string> productModel, [FromQuery] List<string> productInternalMemorySize, [FromQuery] List<string> productMobileRamSize)
        {
            var values = await productService.GetProductRealmePhoneListFilterAsync(productName, productPrice, productModel, productInternalMemorySize, productMobileRamSize);

            foreach (var referee in values)
            {
                await cloudStorageService.GenerateSignedUrlProductRealmePhoneListAsync(referee);
            }

            return Ok(values);
        }
        [HttpGet("GetProductRealmePhoneList")]
        public async Task<IActionResult> GetProductRealmePhoneList()
        {
            var values = await productService.GetProductRealmePhoneListAsync();

            foreach (var referee in values)
            {
                await cloudStorageService.GenerateSignedUrlProductRealmePhoneListAsync(referee);
            }

            return Ok(values);
        }
        [HttpGet("GetProductVivoPhoneList")]
        public async Task<IActionResult> GetProductVivoPhoneList()
        {
            var values = await productService.GetProductVivoPhoneListAsync();

            foreach (var referee in values)
            {
                await cloudStorageService.GenerateSignedUrlProductVivoPhoneListAsync(referee);
            }

            return Ok(values);
        }
        [HttpGet("GetProductVivoPhoneListFilter")]
        public async Task<IActionResult> GetProductVivoPhoneListFilter([FromQuery] List<string> productName, [FromQuery] List<decimal> productPrice, [FromQuery] List<string> productModel, [FromQuery] List<string> productInternalMemorySize, [FromQuery] List<string> productMobileRamSize)
        {
            var values = await productService.GetProductVivoPhoneListFilterAsync(productName, productPrice, productModel, productInternalMemorySize, productMobileRamSize);

            foreach (var referee in values)
            {
                await cloudStorageService.GenerateSignedUrlProductVivoPhoneListAsync(referee);
            }

            return Ok(values);
        }
        [HttpGet("GetProductTecnoCamonPhoneList")]
        public async Task<IActionResult> GetProductTecnoCamonPhoneList()
        {
            var values = await productService.GetProductTecnoCamonPhoneListAsync();

            foreach (var referee in values)
            {
                await cloudStorageService.GenerateSignedUrlProductTecnoCamonPhoneListAsync(referee);
            }

            return Ok(values);
        }
        [HttpGet("GetProductTecnoCamonPhoneListFilter")]
        public async Task<IActionResult> GetProductTecnoCamonPhoneListFilter([FromQuery] List<string> productName, [FromQuery] List<decimal> productPrice, [FromQuery] List<string> productModel, [FromQuery] List<string> productInternalMemorySize, [FromQuery] List<string> productMobileRamSize)
        {
            var values = await productService.GetProductTecnoCamonPhoneListFilterAsync(productName, productPrice, productModel, productInternalMemorySize, productMobileRamSize);

            foreach (var referee in values)
            {
                await cloudStorageService.GenerateSignedUrlProductTecnoCamonPhoneListAsync(referee);
            }

            return Ok(values);
        }
        [HttpGet("GetProductSmartWatchList")]
        public async Task<IActionResult> GetProductSmartWatchList()
        {
            var values = await productService.GetProductSmartWatchListAsync();

            foreach (var referee in values)
            {
                await cloudStorageService.GenerateSignedUrlProductSmartWatchListAsync(referee);
            }

            return Ok(values);
        }
        [HttpGet("GetProductSmartWatchListFilter")]
        public async Task<IActionResult> GetProductSmartWatchListFilter([FromQuery] List<string> productName, [FromQuery] List<decimal> productPrice, [FromQuery] List<string> productColor)
        {
            var values = await productService.GetProductSmartWatchListFilterAsync(productName, productPrice,productColor);

            foreach (var referee in values)
            {
                await cloudStorageService.GenerateSignedUrlProductSmartWatchListAsync(referee);
            }

            return Ok(values);
        }
        [HttpGet("GetProductMemoryCardList")]
        public async Task<IActionResult> GetProductMemoryCardList()
        {
            var values = await productService.GetProductMemoryCardListAsync();

            foreach (var referee in values)
            {
                await cloudStorageService.GenerateSignedUrlProductMemoryCardListAsync(referee);
            }

            return Ok(values);
        }
        [HttpGet("GetProductMemoryCardListFilter")]
        public async Task<IActionResult> GetProductMemoryCardListFilter([FromQuery] List<string> productName, [FromQuery] List<decimal> productPrice, [FromQuery] List<string> productModel, [FromQuery] List<string> productStorage)
        {
            var values = await productService.GetProductMemoryCardListFilterAsync(productName,productPrice,productModel,productStorage);

            foreach (var referee in values)
            {
                await cloudStorageService.GenerateSignedUrlProductMemoryCardListAsync(referee);
            }

            return Ok(values);
        }
        [HttpGet("GetProductLaptopComputerList")]
        public async Task<IActionResult> GetProductLaptopComputerList()
        {
            var values = await productService.GetProductLaptopComputerListAsync();

            foreach (var referee in values)
            {
                await cloudStorageService.GenerateSignedUrlProductLaptopComputerListAsync(referee);
            }

            return Ok(values);
        }
        [HttpGet("GetProductLaptopComputerListFilter")]
        public async Task<IActionResult> GetProductLaptopComputerListFilter([FromQuery] List<string> productName, [FromQuery] List<decimal> productPrice, [FromQuery] List<string> productProcessModel, [FromQuery] List<string> productGrapichCardModel, [FromQuery] List<string> productMemoryRam)
        {
            var values = await productService.GetProductLaptopComputerListFilterAsync(productName,productPrice,productProcessModel,productGrapichCardModel,productMemoryRam);

            foreach (var referee in values)
            {
                await cloudStorageService.GenerateSignedUrlProductLaptopComputerListAsync(referee);
            }

            return Ok(values);
        }
        [HttpGet("GetProductDesktopComputerList")]
        public async Task<IActionResult> GetProductDesktopComputerList()
        {
            var values = await productService.GetProductDesktopComputerListAsync();

            foreach (var referee in values)
            {
                await cloudStorageService.GenerateSignedUrlProductDesktopComputerListAsync(referee);
            }

            return Ok(values);
        }
        [HttpGet("GetProductDesktopComputerListFilter")]
        public async Task<IActionResult> GetProductDesktopComputerListFilter([FromQuery] List<string> productName, [FromQuery] List<decimal> productPrice, [FromQuery] List<string> productProcessModel, [FromQuery] List<string> productGrapichCardModel, [FromQuery] List<string> productMemoryRam)
        {
            var values = await productService.GetProductDesktopComputerListFilterAsync(productName, productPrice, productProcessModel, productGrapichCardModel, productMemoryRam);

            foreach (var referee in values)
            {
                await cloudStorageService.GenerateSignedUrlProductDesktopComputerListAsync(referee);
            }

            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
        {
            if (ModelState.IsValid)
            {
                await cloudStorageService.GoogleStorageProductAdditionControlAsync(createProductDto);
                await productService.CreateProductAsync(createProductDto);
                return Ok("Success");
            }

            return Ok("Error");
        }

        //private string? GenerateFileNameToSave(string incomingFileName)
        //{
        //    var fileName = Path.GetFileNameWithoutExtension(incomingFileName);
        //    var extension = Path.GetExtension(incomingFileName);
        //    return $"{fileName}-{DateTime.Now.ToUniversalTime().ToString("yyyyMMddHHmmss")}{extension}";
        //}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdProduct(string id)
        {
            var value = await productService.GetByIdProductAsync(id);
            return Ok(value);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            await productService.GetCheckProductOrDeleteProductImages(id);
            return Ok("Başarılı");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
        {
            await cloudStorageService.ReplaceProductPhoto(updateProductDto);

            await productService.UpdateProductAsync(updateProductDto);

            return Ok("Succesfull");
        }

        //private async Task ReplacePhoto(UpdateProductDto updateProductDto)
        //{
        //    if (updateProductDto.Photo != null)
        //    {
        //        //replace the file by deleting referee.SavedFileName file and then uploading new referee.Photo
        //        if (!string.IsNullOrEmpty(updateProductDto.SavedFileName))
        //        {
        //            await cloudStorageService.DeleteFileAsync(updateProductDto.SavedFileName);
        //        }
        //        updateProductDto.SavedFileName = GenerateFileNameToSave(updateProductDto.Photo.FileName);
        //        updateProductDto.SavedUrl = await cloudStorageService.UploadFileAsync(updateProductDto.Photo, updateProductDto.SavedFileName);
        //        updateProductDto.ProductImageUrl = await cloudStorageService.GetSignedUrlProductImageAsync(updateProductDto.SavedFileName);
        //    }
        //}

        

        //private async Task GenerateSignedUrlTwo(GetByIdProductDto getByIdProductDto)
        //{
        //    // Get Signed URL only when Saved File Name is available.
        //    if (!string.IsNullOrWhiteSpace(getByIdProductDto.SavedFileName))
        //    {
        //        getByIdProductDto.ProductImageUrl = await cloudStorageService.GetSignedUrlProductImageAsync(getByIdProductDto.SavedFileName);
        //    }
        //}
        //private async Task GenerateSignedUrl(ResultProductWithCategoryDto resultProductWithCategoryDto)
        //{
        //    // Get Signed URL only when Saved File Name is available.
        //    if (!string.IsNullOrWhiteSpace(resultProductWithCategoryDto.SavedFileName))
        //    {
        //        resultProductWithCategoryDto.ProductImageUrl = await cloudStorageService.GetSignedUrlProductImageAsync(resultProductWithCategoryDto.SavedFileName);
        //    }
        //}

        [HttpGet("ProductWithCategoryByIdList/{id}")]
        public async Task<IActionResult> ProductWithCategoryByIdList(string id)
        {
            var values = await productService.GetProductWithCategoryByIdAsync(id);
            return Ok(values);
        }
    }
}
