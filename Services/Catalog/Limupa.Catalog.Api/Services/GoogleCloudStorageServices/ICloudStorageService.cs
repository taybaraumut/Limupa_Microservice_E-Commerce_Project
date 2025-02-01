using Limupa.Catalog.Api.Dtos.ProductDtos;
using Limupa.Catalog.Api.Dtos.ProductImageDtos;
using Limupa.Catalog.Dtos.ProductDtos;
using Limupa.Catalog.Dtos.ProductImageDtos;

namespace Limupa.Catalog.Api.Services.GoogleCloudStorageServices
{
    public interface ICloudStorageService
    {
        Task<string> GetSignedUrlProductImageAsync(string fileNameToRead, int timeOutInMinutes = 30);
        Task<string> UploadFileAsync(IFormFile fileToUpload, string fileNameToSave);
        string? GenerateFileNameToSave(string incomingFileName);
        Task ReplaceProductPhoto(UpdateProductDto updateProductDto);
        Task ReplaceProductImagePhoto(UpdateProductImageDto updateProductImageDto);
        Task GenerateSignedUrlOne(GetByIdProductDto getByIdProductDto);
        Task GenerateSignedUrlTwo(ResultProductWithCategoryDto resultProductWithCategoryDto);
        Task GenerateSignedUrlProductLastTenAsync(ResultProductLastTenDto resultProductLastTenDto);
        Task GenerateSignedUrlProductPersonelCareCategoryAsync(ResultTenDataByProductPersonelCareCategoryDto resultTenDataByProductPersonelCareCategoryDto);
        Task GenerateSignedUrlProductByElectronicDeviceCategoryAsync(ResultProductByElectronicDeviceCategoryDto resultProductByElectronicDeviceCategoryDto);
        Task GenerateSignedUrlProductİpohnePhoneModelAsync(ResultProductİpohnePhoneModelDto resultProductİpohnePhoneModelDto);
        Task GenerateSignedUrlProductMackbookLaptopModelAsync(ResultProductMackbookLaptopModelDto resultProductMackbookLaptopModelDto);
        Task GenerateSignedUrlProductVideoGameModelAsync(ResultProductVideoGameModelDto resultProductVideoGameModelDto);
        Task GenerateSignedUrlProductImageAsync(GetProductImageByProductIdDto  getProductImageByProductIdDto);
        Task GenerateSignedUrlProductBluetoothListAsync(ResultProductBluetoothDto resultProductBluetoothDto);
        Task GenerateSignedUrlProductGeneralPhoneListAsync(ResultProductGeneralPhoneDto resultProductGeneralPhoneDto);
        Task GenerateSignedUrlProductİphonePhoneListAsync(ResultProductİphonePhoneDto resultProductİphonePhoneDto);
        Task GenerateSignedUrlProductSamsungPhoneListAsync(ResultProductSamsungPhoneDto resultProductSamsungPhoneDto);
        Task GenerateSignedUrlProductXiaomiPhoneListAsync(ResultProductXiaomiPhoneDto resultProductXiaomiPhoneDto);
        Task GenerateSignedUrlProductRealmePhoneListAsync(ResultProductRealmePhoneDto resultProductRealmePhoneDto);
        Task GenerateSignedUrlProductVivoPhoneListAsync(ResultProductVivoPhoneDto resultProductVivoPhoneDto);
        Task GenerateSignedUrlProductTecnoCamonPhoneListAsync(ResultProductTecnoCamonPhoneDto resultProductTecnoCamonPhoneDto);
        Task GenerateSignedUrlProductSmartWatchListAsync(ResultProductSmartWatchDto resultProductSmartWatchDto);
        Task GenerateSignedUrlProductMemoryCardListAsync(ResultProductMemoryCardDto resultProductMemoryCardDto);
        Task GenerateSignedUrlProductLaptopComputerListAsync(ResultProductLaptopComputerDto resultProductLaptopComputerDto);
        Task GenerateSignedUrlProductDesktopComputerListAsync(ResultProductDesktopComputerDto resultProductDesktopComputerDto);
        Task GoogleStorageProductImageAdditionControlAsync(CreateProductImageDto createProductImageDto);
        Task GoogleStorageProductAdditionControlAsync(CreateProductDto createProductDto);
        Task DeleteFileAsync(string fileNameToDelete);
    }
}
