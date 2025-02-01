using Limupa.Catalog.Api.Dtos.ProductImageDtos;
using Limupa.Catalog.Dtos.ProductImageDtos;
using Limupa.Catalog.Entities;

namespace Limupa.Catalog.Services.ProductImageServices
{
    public interface IProductImageService
    {
        Task<List<ResultProductImageDto>> GetAllProductImageAsync();
        Task CreateProductImageAsync(CreateProductImageDto createProductImageDto);
        Task UpdateProductImageAsync(UpdateProductImageDto updateProductImageDto);
        Task DeleteProductImageAsync(string id);
        Task<GetByIdProductImageDto> GetByIdProductImageAsync(string id);
        Task<GetProductImageByProductIdDto> GetProductImageByProductIdAsync(string id);
        Task<GetProductImageByProductIdCheckDto> GetProductImageByProductIdCheckAsync(string id);
        Task<List<ResultProductImageWithProductDto>> GetProductImageWithProductAsync();

    }
}
