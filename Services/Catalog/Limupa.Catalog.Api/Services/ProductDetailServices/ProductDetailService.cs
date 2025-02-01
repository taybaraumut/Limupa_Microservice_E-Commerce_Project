using AutoMapper;
using Limupa.Catalog.Api.Dtos.ProductDetailDtos;
using Limupa.Catalog.Dtos.ProductDetailDtos;
using Limupa.Catalog.Entities;
using Limupa.Catalog.Settings;
using MongoDB.Driver;

namespace Limupa.Catalog.Services.ProductDetailServices
{
    public class ProductDetailService : IProductDetailService
    {
        private readonly IMongoCollection<ProductDetail> productdetailCollection;
        private readonly IMongoCollection<Product> productCollection;
        private readonly IMapper mapper;

        public ProductDetailService(IMapper mapper,IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            productdetailCollection = database.GetCollection<ProductDetail>(databaseSettings.ProductDetailCollectionName);
            productCollection = database.GetCollection<Product>(databaseSettings.ProductCollectionName);

            this.mapper = mapper;
        }

        public async Task CreateProductDetailAsync(CreateProductDetailDto createProductDetailDto)
        {
            var value = mapper.Map<ProductDetail>(createProductDetailDto);
            await productdetailCollection.InsertOneAsync(value);
        }

        public async Task DeleteProductDetailAsync(string id)
        {
            await productdetailCollection.DeleteOneAsync(x=>x.ProductDetailID == id);
        }

        public async Task<List<ResultProductDetailDto>> GetAllProductDetailAsync()
        {
            var values = mapper.Map<List<ResultProductDetailDto>>(await productdetailCollection.Find(x=>true).ToListAsync());
            return values;
        }

        public async Task<GetByIdProductDetailDto> GetByIdProductDetailAsync(string id)
        {
            var value = mapper.Map<GetByIdProductDetailDto>(await productdetailCollection.Find<ProductDetail>(x=>x.ProductDetailID==id).FirstOrDefaultAsync());
            return value;
        }
      
        public async Task<GetProductDetailByProductIdDto> GetProductDetailByProductIdAsync(string id)
        {
            var value = await productdetailCollection.Find<ProductDetail>(x => x.ProductUrlSeo == id).FirstOrDefaultAsync();
            return mapper.Map<GetProductDetailByProductIdDto>(value);
        }

        public async Task<List<GetProductDetailWithProductDto>> GetProductDetailWithProductAsync()
        {
            var values = await productdetailCollection.Find(x=>true).ToListAsync();

            foreach (var item in values)
            {
                item.Product = await productCollection.Find<Product>(x=>x.ProductID==item.ProductID).SingleOrDefaultAsync();
            }

            return mapper.Map<List<GetProductDetailWithProductDto>>(values);
        }

        public async Task UpdateProductDetailAsync(UpdateProductDetailDto updateProductDetailDto)
        {
            var value = mapper.Map<ProductDetail>(updateProductDetailDto);
            await productdetailCollection.FindOneAndReplaceAsync(x=>x.ProductDetailID == updateProductDetailDto.ProductDetailID,value);
        }
    }
}
