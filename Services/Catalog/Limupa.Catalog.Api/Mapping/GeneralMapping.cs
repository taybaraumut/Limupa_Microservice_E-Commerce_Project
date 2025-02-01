using AutoMapper;
using Limupa.Catalog.Api.Dtos.AboutDtos;
using Limupa.Catalog.Api.Dtos.ContactDtos;
using Limupa.Catalog.Api.Dtos.FeatureDtos;
using Limupa.Catalog.Api.Dtos.FeatureSliderDtos;
using Limupa.Catalog.Api.Dtos.ProductDetailDtos;
using Limupa.Catalog.Api.Dtos.ProductDtos;
using Limupa.Catalog.Api.Dtos.ProductImageDtos;
using Limupa.Catalog.Api.Dtos.SpecialOfferDtos;
using Limupa.Catalog.Api.Entities;
using Limupa.Catalog.Dtos.CategoryDtos;
using Limupa.Catalog.Dtos.ProductDetailDtos;
using Limupa.Catalog.Dtos.ProductDtos;
using Limupa.Catalog.Dtos.ProductImageDtos;
using Limupa.Catalog.Entities;

namespace Limupa.Catalog.Mapping
{
    public class GeneralMapping:Profile
    {
        public GeneralMapping()
        {
            CreateMap<About, CreateAboutDto>().ReverseMap();
            CreateMap<About, ResultAboutDto>().ReverseMap();
            CreateMap<About, UpdateAboutDto>().ReverseMap();
            CreateMap<About, GetByIdAboutDto>().ReverseMap();
            CreateMap<About, ResultAboutDto>().ReverseMap();

            CreateMap<Product,CreateProductDto>().ReverseMap();
            CreateMap<Product,ResultProductDto>().ReverseMap();
            CreateMap<Product,UpdateProductDto>().ReverseMap();
            CreateMap<Product,GetByIdProductDto>().ReverseMap();
            CreateMap<Product, ResultProductWithCategoryDto>().ReverseMap();
            CreateMap<Product, ResultProductWithCategoryByIdDto>().ReverseMap();
            CreateMap<Product, ResultProductByElectronicDeviceCategoryDto>().ReverseMap();
            CreateMap<Product, ResultProductByHomeAndEntertainmentDeviceCategoryDto>().ReverseMap();
            CreateMap<Product, ResultProductİpohnePhoneModelDto>().ReverseMap();
            CreateMap<Product, ResultProductLastTenDto>().ReverseMap();
            CreateMap<Product, ResultProductMackbookLaptopModelDto>().ReverseMap();
            CreateMap<Product, ResultProductVideoGameModelDto>().ReverseMap();
            CreateMap<Product, ResultTenDataByProductPersonelCareCategoryDto>().ReverseMap();
            CreateMap<Product, ResultProductBluetoothDto>().ReverseMap();
            CreateMap<Product, ResultProductGeneralPhoneDto>().ReverseMap();
            CreateMap<Product, ResultProductİphonePhoneDto>().ReverseMap();
            CreateMap<Product, ResultProductXiaomiPhoneDto>().ReverseMap();
            CreateMap<Product, ResultProductRealmePhoneDto>().ReverseMap();
            CreateMap<Product, ResultProductVivoPhoneDto>().ReverseMap();
            CreateMap<Product, ResultProductTecnoCamonPhoneDto>().ReverseMap();
            CreateMap<Product, ResultProductSmartWatchDto>().ReverseMap();
            CreateMap<Product, ResultProductMemoryCardDto>().ReverseMap();
            CreateMap<Product, ResultProductLaptopComputerDto>().ReverseMap();
            CreateMap<Product, ResultProductDesktopComputerDto>().ReverseMap();

            CreateMap<ProductDetail, CreateProductDetailDto>().ReverseMap();
            CreateMap<ProductDetail, ResultProductDetailDto>().ReverseMap();
            CreateMap<ProductDetail, UpdateProductDetailDto>().ReverseMap();
            CreateMap<ProductDetail, GetByIdProductDetailDto>().ReverseMap();
            CreateMap<ProductDetail, GetProductDetailByProductIdDto>().ReverseMap();
            CreateMap<ProductDetail, GetProductDetailWithProductDto>().ReverseMap();

            CreateMap<ProductImage, CreateProductImageDto>().ReverseMap();
            CreateMap<ProductImage, ResultProductImageDto>().ReverseMap();
            CreateMap<ProductImage, UpdateProductImageDto>().ReverseMap();
            CreateMap<ProductImage, GetByIdProductImageDto>().ReverseMap();
            CreateMap<ProductImage, ResultProductImageWithProductDto>().ReverseMap();
            CreateMap<ProductImage, GetProductImageByProductIdCheckDto>().ReverseMap();
            CreateMap<ProductImage, GetProductImageByProductIdDto>().ReverseMap();

            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, ResultCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();
            CreateMap<Category, GetByIdCategoryDto>().ReverseMap();

            CreateMap<FeatureSlider, CreateFeatureSliderDto>().ReverseMap();
            CreateMap<FeatureSlider, ResultFeatureSliderDto>().ReverseMap();
            CreateMap<FeatureSlider, UpdateFeatureSliderDto>().ReverseMap();
            CreateMap<FeatureSlider, GetByIdFeatureSliderDto>().ReverseMap();

            CreateMap<SpecialOffer, CreateSpecialOfferDto>().ReverseMap();
            CreateMap<SpecialOffer, ResultSpecialOfferDto>().ReverseMap();
            CreateMap<SpecialOffer, UpdateSpecialOfferDto>().ReverseMap();
            CreateMap<SpecialOffer, GetByIdSpecialOfferDto>().ReverseMap();

            CreateMap<Feature, CreateFeatureDto>().ReverseMap();
            CreateMap<Feature, ResultFeatureDto>().ReverseMap();
            CreateMap<Feature, UpdateFeatureDto>().ReverseMap();
            CreateMap<Feature, GetByIdFeatureDto>().ReverseMap();

            CreateMap<Contact, CreateContactDto>().ReverseMap();
            CreateMap<Contact, ResultContactDto>().ReverseMap();
            CreateMap<Contact, UpdateContactDto>().ReverseMap();
            CreateMap<Contact, GetByIdContactDto>().ReverseMap();
        }
    }
}
