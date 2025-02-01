using Limupa.Catalog.Api.Dtos.ProductDetailDtos;
using Limupa.Catalog.Dtos.ProductDetailDtos;
using Limupa.Catalog.Entities;

namespace Limupa.Catalog.Services.ProductDetailServices
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
