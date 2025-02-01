using Limupa.Catalog.Api.Dtos.ProductDtos;
using Limupa.Catalog.Api.Dtos.ProductImageDtos;
using Limupa.Catalog.Dtos.ProductDtos;
using Limupa.Catalog.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Limupa.Catalog.Services.ProductServices
{
    public interface IProductService
    {
        Task<List<ResultProductDto>> GetAllProductAsync();
        Task CreateProductAsync(CreateProductDto createProductDto);
        Task UpdateProductAsync(UpdateProductDto updateProductDto);
        Task<GetByIdProductDto> GetByIdProductAsync(string id);
        Task DeleteProductAsync(string id);
        Task<List<ResultProductWithCategoryDto>> GetProductWithCategoryAsync();
        Task<List<ResultProductWithCategoryByIdDto>> GetProductWithCategoryByIdAsync(string id);
        Task GetCheckProductOrDeleteProductImages(string id);
        Task<List<ResultProductWithCategoryDto>> GetSearchProductAsync(string productCategory, string productTextSearch);
        Task<List<ResultProductLastTenDto>> GetLastTenProductAsync();
        Task<List<ResultTenDataByProductPersonelCareCategoryDto>> GetTenDataByProductPersonelCareCategoryAsync();
        Task<List<ResultProductByElectronicDeviceCategoryDto>> GetProductByElectronicDeviceCategoryAsync();
        Task<List<ResultProductByHomeAndEntertainmentDeviceCategoryDto>> GetProductByHomeAndEntertainmentDeviceCategoryAsync();
        Task<List<ResultProductİpohnePhoneModelDto>> GetProductİpohnePhoneModelAsync();
        Task<List<ResultProductMackbookLaptopModelDto>> GetProductMackbookLaptopModelAsync();
        Task<List<ResultProductVideoGameModelDto>> GetProductVideoGameModelAsync();
        Task<List<ResultProductBluetoothDto>> GetProductBluetoothListAsync();
        Task<List<ResultProductBluetoothDto>> GetProductBluetoothListFilterAsync(List<string> productName,List<decimal> Price,List<string> productModel);
        Task<List<ResultProductGeneralPhoneDto>> GetProductGeneralPhoneListFilterAsync(List<string> productName, List<decimal> productPrice, List<string> productModel, List<string> productInternalMemorySize, List<string> productMobileRamSize);
        Task<List<ResultProductGeneralPhoneDto>> GetProductGeneralPhoneListAsync();
        Task<List<ResultProductİphonePhoneDto>> GetProductİphonePhoneListFilterAsync(List<string> productName, List<decimal> productPrice, List<string> productModel, List<string> productInternalMemorySize);
        Task<List<ResultProductİphonePhoneDto>> GetProductİphonePhoneListAsync();
        Task<List<ResultProductSamsungPhoneDto>> GetProductSamsungPhoneListAsync();
        Task<List<ResultProductSamsungPhoneDto>> GetProductSamsungPhoneListFilterAsync(List<string> productName, List<decimal> productPrice, List<string> productModel, List<string> productInternalMemorySize, List<string> productMobileRamSize);
        Task<List<ResultProductXiaomiPhoneDto>> GetProductXiaomiPhoneListAsync();
        Task<List<ResultProductXiaomiPhoneDto>> GetProductXiaomiPhoneListFilterAsync(List<string> productName, List<decimal> productPrice, List<string> productModel, List<string> productInternalMemorySize, List<string> productMobileRamSize);
        Task<List<ResultProductRealmePhoneDto>> GetProductRealmePhoneListAsync();
        Task<List<ResultProductRealmePhoneDto>> GetProductRealmePhoneListFilterAsync(List<string> productName, List<decimal> productPrice, List<string> productModel, List<string> productInternalMemorySize, List<string> productMobileRamSize);
        Task<List<ResultProductVivoPhoneDto>> GetProductVivoPhoneListAsync();
        Task<List<ResultProductVivoPhoneDto>> GetProductVivoPhoneListFilterAsync(List<string> productName, List<decimal> productPrice, List<string> productModel, List<string> productInternalMemorySize, List<string> productMobileRamSize);
        Task<List<ResultProductTecnoCamonPhoneDto>> GetProductTecnoCamonPhoneListAsync();
        Task<List<ResultProductTecnoCamonPhoneDto>> GetProductTecnoCamonPhoneListFilterAsync(List<string> productName, List<decimal> productPrice, List<string> productModel, List<string> productInternalMemorySize, List<string> productMobileRamSize);
        Task<List<ResultProductSmartWatchDto>> GetProductSmartWatchListAsync();
        Task<List<ResultProductSmartWatchDto>> GetProductSmartWatchListFilterAsync(List<string> productName, List<decimal> productPrice,List<string> productColor);
        Task<List<ResultProductMemoryCardDto>> GetProductMemoryCardListAsync();
        Task<List<ResultProductMemoryCardDto>> GetProductMemoryCardListFilterAsync(List<string> productName, List<decimal> productPrice, List<string> productModel, List<string> productStorage);
        Task<List<ResultProductLaptopComputerDto>> GetProductLaptopComputerListAsync();
        Task<List<ResultProductLaptopComputerDto>> GetProductLaptopComputerListFilterAsync(List<string> productName, List<decimal> productPrice, List<string> productProcessModel, List<string> productGrapichCardModel, List<string> productMemoryRam);
        Task<List<ResultProductDesktopComputerDto>> GetProductDesktopComputerListAsync();
        Task<List<ResultProductDesktopComputerDto>> GetProductDesktopComputerListFilterAsync(List<string> productName, List<decimal> productPrice, List<string> productProcessModel, List<string> productGrapichCardModel, List<string> productMemoryRam);
        //GetProductByLaptopCategory
    }
}
