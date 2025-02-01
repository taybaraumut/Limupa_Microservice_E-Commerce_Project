using AutoMapper;
using Limupa.Catalog.Api.Dtos.ProductDtos;
using Limupa.Catalog.Api.Dtos.ProductImageDtos;
using Limupa.Catalog.Dtos.ProductImageDtos;
using Limupa.Catalog.Entities;
using Limupa.Catalog.Settings;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Limupa.Catalog.Services.ProductImageServices
{
    public class ProductImageService : IProductImageService
    {
        private readonly IMongoCollection<ProductImage> productımageCollection;
        private readonly IMongoCollection<Product> productCollection;
        private readonly IMapper mapper;

        public ProductImageService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            productımageCollection = database.GetCollection<ProductImage>(databaseSettings.ProductImageCollectionName);
            productCollection = database.GetCollection<Product>(databaseSettings.ProductCollectionName);
            this.mapper = mapper;
        }

        public async Task CreateProductImageAsync(CreateProductImageDto createProductImageDto)
        {
            var value = mapper.Map<ProductImage>(createProductImageDto);
            await productımageCollection.InsertOneAsync(value);
        }

        public async Task DeleteProductImageAsync(string id)
        {
            await productımageCollection.DeleteOneAsync(x => x.ProductImageID == id);
        }

        public async Task<List<ResultProductImageDto>> GetAllProductImageAsync()
        {
            var values = mapper.Map<List<ResultProductImageDto>>(await productımageCollection.Find(x => true).ToListAsync());
            return values;
        }

        public async Task<List<ResultProductImageWithProductDto>> GetProductImageWithProductAsync()
        {
            var values = await productımageCollection.Find(x => true).ToListAsync();

            foreach (var item in values)
            {
                item.Product = await productCollection.Find<Product>(x => x.ProductID == item.ProductID).SingleOrDefaultAsync();
            }

            return mapper.Map<List<ResultProductImageWithProductDto>>(values);
        }

        public async Task<GetByIdProductImageDto> GetByIdProductImageAsync(string id)
        {
            var value = mapper.Map<GetByIdProductImageDto>(await productımageCollection.Find<ProductImage>(x => x.ProductImageID == id).FirstOrDefaultAsync());
            return value;
        }

        public async Task<GetProductImageByProductIdDto> GetProductImageByProductIdAsync(string id)
        {
            //var filter = Builders<ProductImage>.Filter.Eq(p => p.ProductID,id);

            //var productsWithBigImageUrls = await productımageCollection.Find(filter).FirstOrDefaultAsync();

            //var productDto = new GetByIdProductImageDto
            //{
            //    ProductImageID = productsWithBigImageUrls.ProductImageID,
            //    ProductID = productsWithBigImageUrls.ProductID,
            //    ProductBigImageUrl= productsWithBigImageUrls.ProductBigImageUrl,
            //    ProductSmallImageUrl=productsWithBigImageUrls.ProductSmallImageUrl
            //};

            var value = mapper.Map<GetProductImageByProductIdDto>(await productımageCollection.Find<ProductImage>(x => x.ProductUrlSeo == id).FirstOrDefaultAsync());
            return value;
        }

        public async Task<GetProductImageByProductIdCheckDto> GetProductImageByProductIdCheckAsync(string id)
        {
            var value = mapper.Map<GetProductImageByProductIdCheckDto>(await productımageCollection.Find<ProductImage>(x => x.ProductUrlSeo == id).FirstOrDefaultAsync());

            if (value != null)
            {
                return value;
            }

            return null!;
        }

        public async Task UpdateProductImageAsync(UpdateProductImageDto updateProductImageDto)
        {
            var value = mapper.Map<ProductImage>(updateProductImageDto);
            await productımageCollection.FindOneAndReplaceAsync(x => x.ProductImageID == updateProductImageDto.ProductImageID, value);
        }       
    }
}
