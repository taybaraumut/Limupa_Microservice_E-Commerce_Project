using Limupa.DtoLayer.ProductImageDtos;

namespace Limupa.UI.Services.CatalogServices.ProductImageServices
{
    public class ProductImageService : IProductImageService
    {
        private readonly HttpClient httpClient;

        public ProductImageService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task CreateProductImageAsync(CreateProductImageDto createProductImageDto)
        {
            await httpClient.PostAsJsonAsync("productimages", createProductImageDto);
        }

        public async Task DeleteProductImageAsync(string id)
        {
            await httpClient.DeleteAsync("productimages/" + id);
        }

        public async Task<List<ResultProductImageDto>> GetAllProductImageAsync()
        {
            var responseMessage = await httpClient.GetAsync("productimages");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductImageDto>>();
            return values;
        }

        public async Task<GetByIdProductImageDto> GetByIdProductImageAsync(string id)
        {
            var responseMessage = await httpClient.GetAsync("productimages/" + id);
            var value = await responseMessage.Content.ReadFromJsonAsync<GetByIdProductImageDto>();
            return value;
        }

        public async Task<GetByIdProductImageDto> GetProductImageByProductIdAsync(string id)
        {
            var responseMessage = await httpClient.GetAsync("productimages/ProductImageByProductId/"+id);
            var values = await responseMessage.Content.ReadFromJsonAsync<GetByIdProductImageDto>();
            return values;
        }

        public async Task<GetProductImageByProductIdCheckDto> GetProductImageByProductIdCheckAsync(string id)
        {
            var responseMessage = await httpClient.GetAsync("productimages/ProductImageByProductIdCheck/" + id);
            var value = await responseMessage.Content.ReadFromJsonAsync<GetProductImageByProductIdCheckDto>();
            return value;
        }

        public async Task<List<ResultProductImageWithProductDto>> GetProductImageWithProductAsync()
        {
            var responseMessage = await httpClient.GetAsync("productimages/ProductImageWithProductList");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductImageWithProductDto>>();
            return values;
        }

        public async Task UpdateProductImageAsync(UpdateProductImageDto updateProductImageDto)
        {
            await httpClient.PutAsJsonAsync("productimages", updateProductImageDto);
        }
    }
}
