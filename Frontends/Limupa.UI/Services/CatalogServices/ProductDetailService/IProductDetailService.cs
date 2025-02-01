using Limupa.DtoLayer.ProductDetailDtos;

namespace Limupa.UI.Services.CatalogServices.ProductDetailService
{
    public interface IProductDetailService
    {
        Task<List<ResultProductDetailDto>> GetAllProductDetailAsync();
        Task CreateProductDetailAsync(CreateProductDetailDto createProductDetailDto);
        Task UpdateProductDetailAsync(UpdateProductDetailDto updateProductDetailDto);
        Task DeleteProductDetailAsync(string id);
        Task<GetByIdProductDetailDto> GetByIdProductDetailAsync(string id);
        Task<GetProductDetailByProductIdDto> GetProductDetailByProductIdAsync(string id);
        Task<List<GetProductDetailWithProductDto>> GetProductDetailWithProductAsync();
    }
}
