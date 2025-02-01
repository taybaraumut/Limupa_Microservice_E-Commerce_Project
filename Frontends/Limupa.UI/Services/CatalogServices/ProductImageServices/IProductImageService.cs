using Limupa.DtoLayer.ProductImageDtos;

namespace Limupa.UI.Services.CatalogServices.ProductImageServices
{
    public interface IProductImageService
    {
        Task<List<ResultProductImageDto>> GetAllProductImageAsync();
        Task CreateProductImageAsync(CreateProductImageDto createProductImageDto);
        Task UpdateProductImageAsync(UpdateProductImageDto updateProductImageDto);
        Task DeleteProductImageAsync(string id);
        Task<GetByIdProductImageDto> GetByIdProductImageAsync(string id);
        Task<GetByIdProductImageDto> GetProductImageByProductIdAsync(string id);
        Task<GetProductImageByProductIdCheckDto> GetProductImageByProductIdCheckAsync(string id);
        Task<List<ResultProductImageWithProductDto>> GetProductImageWithProductAsync();
    }
}
