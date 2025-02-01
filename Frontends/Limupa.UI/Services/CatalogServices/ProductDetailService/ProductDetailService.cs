using IdentityModel;
using Limupa.DtoLayer.ProductDetailDtos;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Newtonsoft.Json.Linq;
using NuGet.Protocol.Plugins;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;

namespace Limupa.UI.Services.CatalogServices.ProductDetailService
{
    public class ProductDetailService : IProductDetailService
    {
        private readonly HttpClient httpClient;

        public ProductDetailService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task CreateProductDetailAsync(CreateProductDetailDto createProductDetailDto)
        {
            await httpClient.PostAsJsonAsync("productdetails", createProductDetailDto);
        }

        public async Task DeleteProductDetailAsync(string id)
        {
            await httpClient.DeleteAsync("productdetails/" + id);
        }

        public async Task<List<ResultProductDetailDto>> GetAllProductDetailAsync()
        {
            var responseMessage = await httpClient.GetAsync("productdetails");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultProductDetailDto>>();
            return values;
        }

        public async Task<GetByIdProductDetailDto> GetByIdProductDetailAsync(string id)
        {
            var responseMessage = await httpClient.GetAsync("productdetails/" + id);
            var value = await responseMessage.Content.ReadFromJsonAsync<GetByIdProductDetailDto>();
            return value;
        }

        public async Task<GetProductDetailByProductIdDto> GetProductDetailByProductIdAsync(string id)
        {
            var responseMessage = await httpClient.GetAsync("productdetails/ProductDetailByProductId/" + id);
            var value = await responseMessage.Content.ReadFromJsonAsync<GetProductDetailByProductIdDto>();
            return value!;
        }

        public async Task<List<GetProductDetailWithProductDto>> GetProductDetailWithProductAsync()
        {
            var responseMessage = await httpClient.GetAsync("productdetails/ProductDetailWithProductList");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<GetProductDetailWithProductDto>>();
            return values;
        }

        public async Task UpdateProductDetailAsync(UpdateProductDetailDto updateProductDetailDto)
        {
            await httpClient.PutAsJsonAsync("productdetails", updateProductDetailDto);
        }
    }
}
