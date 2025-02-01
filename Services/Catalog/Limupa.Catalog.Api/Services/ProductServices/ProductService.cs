using AutoMapper;
using Limupa.Basket.Api.Services.GoogleCloudStorageServices;
using Limupa.Catalog.Api.Dtos.ProductDtos;
using Limupa.Catalog.Api.Dtos.ProductImageDtos;
using Limupa.Catalog.Dtos.CategoryDtos;
using Limupa.Catalog.Dtos.ProductDtos;
using Limupa.Catalog.Entities;
using Limupa.Catalog.Settings;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using System.Threading;
using static MongoDB.Driver.WriteConcern;

namespace Limupa.Catalog.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> productCollection;
        private readonly IMongoCollection<Category> categoryCollection;
        private readonly IMongoCollection<ProductImage> productImageCollection;
        private readonly IMongoCollection<ProductDetail> productDetailCollection;
        private readonly IMapper mapper;

        public ProductService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            productCollection = database.GetCollection<Product>(databaseSettings.ProductCollectionName);
            categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            productImageCollection = database.GetCollection<ProductImage>(databaseSettings.ProductImageCollectionName);
            productDetailCollection = database.GetCollection<ProductDetail>(databaseSettings.ProductDetailCollectionName);

            this.mapper = mapper;
        }

        public async Task CreateProductAsync(CreateProductDto createProductDto)
        {
            var value = mapper.Map<Product>(createProductDto);
            await productCollection.InsertOneAsync(value);

        }

        public async Task DeleteProductAsync(string id)
        {
            await productCollection.DeleteOneAsync(x => x.ProductID == id);
        }

        public async Task<List<ResultProductDto>> GetAllProductAsync()
        {
            var values = mapper.Map<List<ResultProductDto>>(await productCollection.Find(x => true).ToListAsync());
            return values;
        }

        public async Task<GetByIdProductDto> GetByIdProductAsync(string id)
        {
            var value = mapper.Map<GetByIdProductDto>(await productCollection.Find<Product>(x => x.ProductUrlSeo == id).FirstOrDefaultAsync());
            return value;
        }
        public async Task<List<ResultProductWithCategoryDto>> GetProductWithCategoryAsync()
        {
            var values = await productCollection.Find(x => true).ToListAsync();

            foreach (var item in values)
            {
                item.Category = await categoryCollection.Find<Category>(x => x.CategoryID == item.CategoryID).SingleOrDefaultAsync();
            }

            return mapper.Map<List<ResultProductWithCategoryDto>>(values);
        }

        public async Task<List<ResultProductWithCategoryByIdDto>> GetProductWithCategoryByIdAsync(string id)
        {
            var values = await productCollection.Find(x => x.CategoryID == id).ToListAsync();

            foreach (var item in values)
            {
                item.Category = await categoryCollection.Find<Category>(x => x.CategoryID == item.CategoryID).SingleOrDefaultAsync();
            }

            return mapper.Map<List<ResultProductWithCategoryByIdDto>>(values);
        }

        public async Task UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            var value = mapper.Map<Product>(updateProductDto);
            await productCollection.FindOneAndReplaceAsync(x => x.ProductID == updateProductDto.ProductID, value);
        }

        public async Task GetCheckProductOrDeleteProductImages(string id)
        {
            await productCollection.DeleteOneAsync(x => x.ProductID == id);

            await productImageCollection.DeleteManyAsync(x => x.ProductID == id);

            await productDetailCollection.DeleteManyAsync(x => x.ProductID == id);
        }

        public async Task<List<ResultProductLastTenDto>> GetLastTenProductAsync()
        {
            var values = await productCollection.Find(x => true).Limit(10).ToListAsync();

            foreach (var item in values)
            {
                item.Category = await categoryCollection.Find<Category>(x => x.CategoryID == item.CategoryID).SingleOrDefaultAsync();
            }

            return mapper.Map<List<ResultProductLastTenDto>>(values);
        }

        public async Task<List<ResultTenDataByProductPersonelCareCategoryDto>> GetTenDataByProductPersonelCareCategoryAsync()
        {
            var categoryName = "Kişisel Bakım Ürünü";
            // Kategori adına göre kategori ID'sini bul
            var filterCategory = Builders<Category>.Filter.Eq(x => x.CategoryName, categoryName);
            var category = await categoryCollection.Find(filterCategory).FirstOrDefaultAsync();

            if (category != null)
            {
                var filterProducts = Builders<Product>.Filter.Eq(x => x.CategoryID, category.CategoryID.ToString());
                var values = await productCollection.Find(filterProducts).Limit(10).ToListAsync();

                foreach (var item in values)
                {
                    item.Category = await categoryCollection.Find<Category>(x => x.CategoryID == item.CategoryID).SingleOrDefaultAsync();
                }

                return mapper.Map<List<ResultTenDataByProductPersonelCareCategoryDto>>(values);
            }
            return null;
        }

        public async Task<List<ResultProductByElectronicDeviceCategoryDto>> GetProductByElectronicDeviceCategoryAsync()
        {
            var categoryName = "Elektronik Cihaz";
            var filterCategory = Builders<Category>.Filter.Eq(x => x.CategoryName, categoryName);
            var category = await categoryCollection.Find(filterCategory).FirstOrDefaultAsync();

            if (category != null)
            {
                var filterProducts = Builders<Product>.Filter.Eq(x => x.CategoryID, category.CategoryID.ToString());
                var values = await productCollection.Find(filterProducts).ToListAsync();

                foreach (var item in values)
                {
                    item.Category = await categoryCollection.Find(x => x.CategoryID == item.CategoryID).SingleOrDefaultAsync();
                }

                return mapper.Map<List<ResultProductByElectronicDeviceCategoryDto>>(values);
            }
            return null;
        }

        public async Task<List<ResultProductByHomeAndEntertainmentDeviceCategoryDto>> GetProductByHomeAndEntertainmentDeviceCategoryAsync()
        {
            var categoryName = "Ev Eğlence Cihazı";
            var filterCategory = Builders<Category>.Filter.Eq(x => x.CategoryName, categoryName);
            var category = await categoryCollection.Find(filterCategory).FirstOrDefaultAsync();

            if (category != null)
            {
                var filterProducts = Builders<Product>.Filter.Eq(x => x.CategoryID, category.CategoryID);
                var values = await productCollection.Find(filterProducts).ToListAsync();

                foreach (var item in values)
                {
                    item.Category = await categoryCollection.Find(x => x.CategoryID == item.CategoryID).SingleOrDefaultAsync();
                }

                return mapper.Map<List<ResultProductByHomeAndEntertainmentDeviceCategoryDto>>(values);
            }
            return null;
        }

        public async Task<List<ResultProductİpohnePhoneModelDto>> GetProductİpohnePhoneModelAsync()
        {
            var filter = Builders<Product>.Filter.Regex(x => x.ProductName, new BsonRegularExpression("iphone", "i"));
            var values = await productCollection.Find(filter).ToListAsync();
            foreach (var item in values)
            {
                item.Category = await categoryCollection.Find(x => x.CategoryID == item.CategoryID).SingleOrDefaultAsync();
            }
            return mapper.Map<List<ResultProductİpohnePhoneModelDto>>(values);
        }

        public async Task<List<ResultProductMackbookLaptopModelDto>> GetProductMackbookLaptopModelAsync()
        {
            var filter = Builders<Product>.Filter.Regex(x => x.ProductName, new BsonRegularExpression("macbook", "i"));
            var values = await productCollection.Find(filter).ToListAsync();
            foreach (var item in values)
            {
                item.Category = await categoryCollection.Find(x => x.CategoryID == item.CategoryID).SingleOrDefaultAsync();
            }
            return mapper.Map<List<ResultProductMackbookLaptopModelDto>>(values);
        }

        public async Task<List<ResultProductVideoGameModelDto>> GetProductVideoGameModelAsync()
        {
            var categoryName = "Video Oyunu";
            var filterCategory = Builders<Category>.Filter.Eq(x => x.CategoryName, categoryName);
            var category = await categoryCollection.Find(filterCategory).FirstOrDefaultAsync();

            if (category != null)
            {
                var filterProducts = Builders<Product>.Filter.Eq(x => x.CategoryID, category.CategoryID);
                var values = await productCollection.Find(filterProducts).ToListAsync();

                foreach (var item in values)
                {
                    item.Category = await categoryCollection.Find(x => x.CategoryID == item.CategoryID).SingleOrDefaultAsync();
                }

                return mapper.Map<List<ResultProductVideoGameModelDto>>(values);
            }
            return null;
        }


        public async Task<List<ResultProductBluetoothDto>> GetProductBluetoothListFilterAsync(List<string> productName, List<decimal> productPrice, List<string> productModel)
        {
            try
            {
                var filterBuilder = Builders<Product>.Filter;
                var filterDefinitions = new List<FilterDefinition<Product>>();

                var productNameFilter = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression("bluetooth","i"));
                filterDefinitions.Add(productNameFilter);

                // Fiyat aralığı filtresi
                if (productPrice != null && productPrice.Count >= 2)
                {
                    var priceRangeFilters = new List<FilterDefinition<Product>>();

                    // price listesi ikişerli olarak işlenir
                    for (int i = 0; i < productPrice.Count; i += 2)
                    {
                        decimal minPrice = productPrice[i];
                        decimal maxPrice = productPrice[i + 1];

                        var priceFilter = filterBuilder.And(
                            filterBuilder.Gte(x => x.ProductPrice, minPrice), // Minimum fiyat
                            filterBuilder.Lte(x => x.ProductPrice, maxPrice)  // Maksimum fiyat
                        );

                        priceRangeFilters.Add(priceFilter);
                    }

                    // Fiyat aralığı filtrelerini birleştir (herhangi biri geçerli ise)
                    if (priceRangeFilters.Count > 0)
                    {
                        var priceFiltersCombined = filterBuilder.Or(priceRangeFilters);
                        filterDefinitions.Add(priceFiltersCombined);
                    }
                }

                // Keyword filtresi
                if (productName != null && productName.Count > 0)
                {
                    var keywordFilters = new List<FilterDefinition<Product>>();

                    foreach (var product_name in productName)
                    {
                        var regexFilterProductName = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression(product_name, "i"));
                        keywordFilters.Add(regexFilterProductName);
                    }

                    // Keyword filtrelerini birleştir (herhangi biri geçerli ise)
                    if (keywordFilters.Count > 0)
                    {
                        var keywordFiltersCombined = filterBuilder.Or(keywordFilters);
                        filterDefinitions.Add(keywordFiltersCombined);
                    }
                }

                if (productModel != null && productModel.Count > 0)
                {
                    var keywordFilters = new List<FilterDefinition<Product>>();

                    foreach(var product_model in productModel)
                    {
                        var regexFilterProductModel = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression(product_model, "i"));
                        keywordFilters.Add(regexFilterProductModel);
                    }

                    if (keywordFilters.Count > 0)
                    {
                        var keywordFiltersCombined = filterBuilder.Or(keywordFilters);
                        filterDefinitions.Add(keywordFiltersCombined);
                    }

                }

                // Tüm filtreleri birleştir (herhangi biri geçerli ise)
                var combinedFilter = filterBuilder.And(filterDefinitions);

                // Ürünleri filtreleyerek çek
                var products = await productCollection.Find(combinedFilter).ToListAsync();

                // Her bir ürün için kategori bilgisini al
                foreach (var product in products)
                {
                    product.Category = await categoryCollection.Find(c => c.CategoryID == product.CategoryID)
                                                                .SingleOrDefaultAsync();
                }

                // DTO'ya dönüştürme
                var resultDtoList = mapper.Map<List<ResultProductBluetoothDto>>(products);

                return resultDtoList;
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                Console.WriteLine($"Exception while getting filtered products: {ex.Message}");
                return null; // veya hata durumunu nasıl yönetmek istiyorsanız ona göre bir değer döndürebilirsiniz
            }
        }

        public async Task<List<ResultProductBluetoothDto>> GetProductBluetoothListAsync()
        {
            var filter = Builders<Product>.Filter.Regex(x => x.ProductName,new BsonRegularExpression("bluetooth","i"));
            var values = await productCollection.Find(filter).ToListAsync();
            return mapper.Map<List<ResultProductBluetoothDto>>(values);
        }

        public async Task<List<ResultProductGeneralPhoneDto>> GetProductGeneralPhoneListFilterAsync(List<string> productName, List<decimal> productPrice, List<string> productModel, List<string> productInternalMemorySize, List<string> productMobileRamSize)
        {
            try
            {
                var filterBuilder = Builders<Product>.Filter;
                var filterDefinitions = new List<FilterDefinition<Product>>();

                var productNameFilter = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression("telefon", "i"));
                filterDefinitions.Add(productNameFilter);

                // Fiyat aralığı filtresi
                if (productPrice != null && productPrice.Count >= 2)
                {
                    var priceRangeFilters = new List<FilterDefinition<Product>>();

                    // price listesi ikişerli olarak işlenir
                    for (int i = 0; i < productPrice.Count; i += 2)
                    {
                        decimal minPrice = productPrice[i];
                        decimal maxPrice = productPrice[i + 1];

                        var priceFilter = filterBuilder.And(
                            filterBuilder.Gte(x => x.ProductPrice, minPrice), // Minimum fiyat
                            filterBuilder.Lte(x => x.ProductPrice, maxPrice)  // Maksimum fiyat
                        );

                        priceRangeFilters.Add(priceFilter);
                    }

                    // Fiyat aralığı filtrelerini birleştir (herhangi biri geçerli ise)
                    if (priceRangeFilters.Count > 0)
                    {
                        var priceFiltersCombined = filterBuilder.Or(priceRangeFilters);
                        filterDefinitions.Add(priceFiltersCombined);
                    }
                }

                // Keyword filtresi
                if (productName != null && productName.Count > 0)
                {
                    var keywordFilters = new List<FilterDefinition<Product>>();

                    foreach (var product_name in productName)
                    {
                        var regexFilterProductName = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression(product_name, "i"));
                        keywordFilters.Add(regexFilterProductName);
                    }

                    // Keyword filtrelerini birleştir (herhangi biri geçerli ise)
                    if (keywordFilters.Count > 0)
                    {
                        var keywordFiltersCombined = filterBuilder.Or(keywordFilters);
                        filterDefinitions.Add(keywordFiltersCombined);
                    }
                }

                if (productModel != null && productModel.Count > 0)
                {
                    var keywordFilters = new List<FilterDefinition<Product>>();

                    foreach (var product_model in productModel)
                    {
                        var regexFilterProductModel = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression(product_model, "i"));
                        keywordFilters.Add(regexFilterProductModel);
                    }

                    if (keywordFilters.Count > 0)
                    {
                        var keywordFiltersCombined = filterBuilder.Or(keywordFilters);
                        filterDefinitions.Add(keywordFiltersCombined);
                    }

                }

                if (productInternalMemorySize != null && productInternalMemorySize.Count > 0)
                {
                    var keywordFilters = new List<FilterDefinition<Product>>();

                    foreach (var product_ınternal_memory_size in productInternalMemorySize)
                    {
                        var regexFilterProductModel = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression(product_ınternal_memory_size, "i"));
                        keywordFilters.Add(regexFilterProductModel);
                    }

                    if (keywordFilters.Count > 0)
                    {
                        var keywordFiltersCombined = filterBuilder.Or(keywordFilters);
                        filterDefinitions.Add(keywordFiltersCombined);
                    }

                }

                if (productMobileRamSize != null && productMobileRamSize.Count > 0)
                {
                    var keywordFilters = new List<FilterDefinition<Product>>();

                    foreach (var product_mobile_ram_size in productMobileRamSize)
                    {
                        var regexFilterProductModel = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression(product_mobile_ram_size, "i"));
                        keywordFilters.Add(regexFilterProductModel);
                    }

                    if (keywordFilters.Count > 0)
                    {
                        var keywordFiltersCombined = filterBuilder.Or(keywordFilters);
                        filterDefinitions.Add(keywordFiltersCombined);
                    }

                }

                // Tüm filtreleri birleştir (herhangi biri geçerli ise)
                var combinedFilter = filterBuilder.And(filterDefinitions);

                // Ürünleri filtreleyerek çek
                var products = await productCollection.Find(combinedFilter).ToListAsync();

                // Her bir ürün için kategori bilgisini al
                foreach (var product in products)
                {
                    product.Category = await categoryCollection.Find(c => c.CategoryID == product.CategoryID)
                                                                .SingleOrDefaultAsync();
                }

                // DTO'ya dönüştürme
                var resultDtoList = mapper.Map<List<ResultProductGeneralPhoneDto>>(products);

                return resultDtoList;
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                Console.WriteLine($"Exception while getting filtered products: {ex.Message}");
                return null; // veya hata durumunu nasıl yönetmek istiyorsanız ona göre bir değer döndürebilirsiniz
            }
        }

        public async Task<List<ResultProductGeneralPhoneDto>> GetProductGeneralPhoneListAsync()
        {
            var filter = Builders<Product>.Filter.Regex(x => x.ProductName, new BsonRegularExpression("telefon", "i"));
            var values = await productCollection.Find(filter).ToListAsync();

            foreach(var item in values)
            {
                item.Category = await categoryCollection.Find(x=>x.CategoryID == item.CategoryID).SingleOrDefaultAsync();
            }

            return mapper.Map<List<ResultProductGeneralPhoneDto>>(values);
        }

        public async Task<List<ResultProductİphonePhoneDto>> GetProductİphonePhoneListFilterAsync(List<string> productName, List<decimal> productPrice, List<string> productModel, List<string> productInternalMemorySize)
        {
            try
            {
                var filterBuilder = Builders<Product>.Filter;
                var filterDefinitions = new List<FilterDefinition<Product>>();

                var productNameFilter = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression("iphone", "i"));
                filterDefinitions.Add(productNameFilter);

                // Fiyat aralığı filtresi
                if (productPrice != null && productPrice.Count >= 2)
                {
                    var priceRangeFilters = new List<FilterDefinition<Product>>();

                    // price listesi ikişerli olarak işlenir
                    for (int i = 0; i < productPrice.Count; i += 2)
                    {
                        decimal minPrice = productPrice[i];
                        decimal maxPrice = productPrice[i + 1];

                        var priceFilter = filterBuilder.And(
                            filterBuilder.Gte(x => x.ProductPrice, minPrice), // Minimum fiyat
                            filterBuilder.Lte(x => x.ProductPrice, maxPrice)  // Maksimum fiyat
                        );

                        priceRangeFilters.Add(priceFilter);
                    }

                    // Fiyat aralığı filtrelerini birleştir (herhangi biri geçerli ise)
                    if (priceRangeFilters.Count > 0)
                    {
                        var priceFiltersCombined = filterBuilder.Or(priceRangeFilters);
                        filterDefinitions.Add(priceFiltersCombined);
                    }
                }

                // Keyword filtresi
                if (productName != null && productName.Count > 0)
                {
                    var keywordFilters = new List<FilterDefinition<Product>>();

                    foreach (var product_name in productName)
                    {
                        var regexFilterProductName = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression(product_name, "i"));
                        keywordFilters.Add(regexFilterProductName);
                    }

                    // Keyword filtrelerini birleştir (herhangi biri geçerli ise)
                    if (keywordFilters.Count > 0)
                    {
                        var keywordFiltersCombined = filterBuilder.Or(keywordFilters);
                        filterDefinitions.Add(keywordFiltersCombined);
                    }
                }

                if (productModel != null && productModel.Count > 0)
                {
                    var keywordFilters = new List<FilterDefinition<Product>>();

                    foreach (var product_model in productModel)
                    {
                        var regexFilterProductModel = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression(product_model, "i"));
                        keywordFilters.Add(regexFilterProductModel);
                    }

                    if (keywordFilters.Count > 0)
                    {
                        var keywordFiltersCombined = filterBuilder.Or(keywordFilters);
                        filterDefinitions.Add(keywordFiltersCombined);
                    }

                }

                if (productInternalMemorySize != null && productInternalMemorySize.Count > 0)
                {
                    var keywordFilters = new List<FilterDefinition<Product>>();

                    foreach (var product_ınternal_memory_size in productInternalMemorySize)
                    {
                        var regexFilterProductModel = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression(product_ınternal_memory_size, "i"));
                        keywordFilters.Add(regexFilterProductModel);
                    }

                    if (keywordFilters.Count > 0)
                    {
                        var keywordFiltersCombined = filterBuilder.Or(keywordFilters);
                        filterDefinitions.Add(keywordFiltersCombined);
                    }

                }
               
                // Tüm filtreleri birleştir (herhangi biri geçerli ise)
                var combinedFilter = filterBuilder.And(filterDefinitions);

                // Ürünleri filtreleyerek çek
                var products = await productCollection.Find(combinedFilter).ToListAsync();

                // Her bir ürün için kategori bilgisini al
                foreach (var product in products)
                {
                    product.Category = await categoryCollection.Find(c => c.CategoryID == product.CategoryID)
                                                                .SingleOrDefaultAsync();
                }

                // DTO'ya dönüştürme
                var resultDtoList = mapper.Map<List<ResultProductİphonePhoneDto>>(products);

                return resultDtoList;
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                Console.WriteLine($"Exception while getting filtered products: {ex.Message}");
                return null; // veya hata durumunu nasıl yönetmek istiyorsanız ona göre bir değer döndürebilirsiniz
            }
        }

        public async Task<List<ResultProductİphonePhoneDto>> GetProductİphonePhoneListAsync()
        {
            var filter = Builders<Product>.Filter.Regex(x => x.ProductName, new BsonRegularExpression("iphone", "i"));
            var values = await productCollection.Find(filter).ToListAsync();

            foreach (var item in values)
            {
                item.Category = await categoryCollection.Find(x => x.CategoryID == item.CategoryID).SingleOrDefaultAsync();
            }

            return mapper.Map<List<ResultProductİphonePhoneDto>>(values);
        }

        public async Task<List<ResultProductSamsungPhoneDto>> GetProductSamsungPhoneListAsync()
        {
            var filter = Builders<Product>.Filter.Regex(x => x.ProductName, new BsonRegularExpression("samsung", "i"));
            var values = await productCollection.Find(filter).ToListAsync();

            foreach (var item in values)
            {
                item.Category = await categoryCollection.Find(x => x.CategoryID == item.CategoryID).SingleOrDefaultAsync();
            }

            return mapper.Map<List<ResultProductSamsungPhoneDto>>(values);
        }

        public async Task<List<ResultProductSamsungPhoneDto>> GetProductSamsungPhoneListFilterAsync(List<string> productName, List<decimal> productPrice, List<string> productModel, List<string> productInternalMemorySize, List<string> productMobileRamSize)
        {
            try
            {
                var filterBuilder = Builders<Product>.Filter;
                var filterDefinitions = new List<FilterDefinition<Product>>();

                var productNameFilter = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression("samsung", "i"));
                filterDefinitions.Add(productNameFilter);

                // Fiyat aralığı filtresi
                if (productPrice != null && productPrice.Count >= 2)
                {
                    var priceRangeFilters = new List<FilterDefinition<Product>>();

                    // price listesi ikişerli olarak işlenir
                    for (int i = 0; i < productPrice.Count; i += 2)
                    {
                        decimal minPrice = productPrice[i];
                        decimal maxPrice = productPrice[i + 1];

                        var priceFilter = filterBuilder.And(
                            filterBuilder.Gte(x => x.ProductPrice, minPrice), // Minimum fiyat
                            filterBuilder.Lte(x => x.ProductPrice, maxPrice)  // Maksimum fiyat
                        );

                        priceRangeFilters.Add(priceFilter);
                    }

                    // Fiyat aralığı filtrelerini birleştir (herhangi biri geçerli ise)
                    if (priceRangeFilters.Count > 0)
                    {
                        var priceFiltersCombined = filterBuilder.Or(priceRangeFilters);
                        filterDefinitions.Add(priceFiltersCombined);
                    }
                }

                // Keyword filtresi
                if (productName != null && productName.Count > 0)
                {
                    var keywordFilters = new List<FilterDefinition<Product>>();

                    foreach (var product_name in productName)
                    {
                        var regexFilterProductName = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression(product_name, "i"));
                        keywordFilters.Add(regexFilterProductName);
                    }

                    // Keyword filtrelerini birleştir (herhangi biri geçerli ise)
                    if (keywordFilters.Count > 0)
                    {
                        var keywordFiltersCombined = filterBuilder.Or(keywordFilters);
                        filterDefinitions.Add(keywordFiltersCombined);
                    }
                }

                if (productModel != null && productModel.Count > 0)
                {
                    var keywordFilters = new List<FilterDefinition<Product>>();

                    foreach (var product_model in productModel)
                    {
                        var regexFilterProductModel = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression(product_model, "i"));
                        keywordFilters.Add(regexFilterProductModel);
                    }

                    if (keywordFilters.Count > 0)
                    {
                        var keywordFiltersCombined = filterBuilder.Or(keywordFilters);
                        filterDefinitions.Add(keywordFiltersCombined);
                    }

                }

                if (productInternalMemorySize != null && productInternalMemorySize.Count > 0)
                {
                    var keywordFilters = new List<FilterDefinition<Product>>();

                    foreach (var product_ınternal_memory_size in productInternalMemorySize)
                    {
                        var regexFilterProductModel = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression(product_ınternal_memory_size, "i"));
                        keywordFilters.Add(regexFilterProductModel);
                    }

                    if (keywordFilters.Count > 0)
                    {
                        var keywordFiltersCombined = filterBuilder.Or(keywordFilters);
                        filterDefinitions.Add(keywordFiltersCombined);
                    }

                }

                if (productMobileRamSize != null && productMobileRamSize.Count > 0)
                {
                    var keywordFilters = new List<FilterDefinition<Product>>();

                    foreach (var product_mobile_ram_size in productMobileRamSize)
                    {
                        var regexFilterProductModel = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression(product_mobile_ram_size, "i"));
                        keywordFilters.Add(regexFilterProductModel);
                    }

                    if (keywordFilters.Count > 0)
                    {
                        var keywordFiltersCombined = filterBuilder.Or(keywordFilters);
                        filterDefinitions.Add(keywordFiltersCombined);
                    }

                }

                // Tüm filtreleri birleştir (herhangi biri geçerli ise)
                var combinedFilter = filterBuilder.And(filterDefinitions);

                // Ürünleri filtreleyerek çek
                var products = await productCollection.Find(combinedFilter).ToListAsync();

                // Her bir ürün için kategori bilgisini al
                foreach (var product in products)
                {
                    product.Category = await categoryCollection.Find(c => c.CategoryID == product.CategoryID)
                                                                .SingleOrDefaultAsync();
                }

                // DTO'ya dönüştürme
                var resultDtoList = mapper.Map<List<ResultProductSamsungPhoneDto>>(products);

                return resultDtoList;
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                Console.WriteLine($"Exception while getting filtered products: {ex.Message}");
                return null; // veya hata durumunu nasıl yönetmek istiyorsanız ona göre bir değer döndürebilirsiniz
            }
        }

        public async Task<List<ResultProductXiaomiPhoneDto>> GetProductXiaomiPhoneListAsync()
        {
            var filter = Builders<Product>.Filter.Regex(x => x.ProductName, new BsonRegularExpression("xiaomi", "i"));
            var values = await productCollection.Find(filter).ToListAsync();

            foreach (var item in values)
            {
                item.Category = await categoryCollection.Find(x => x.CategoryID == item.CategoryID).SingleOrDefaultAsync();
            }

            return mapper.Map<List<ResultProductXiaomiPhoneDto>>(values);
        }

        public async Task<List<ResultProductXiaomiPhoneDto>> GetProductXiaomiPhoneListFilterAsync(List<string> productName, List<decimal> productPrice, List<string> productModel, List<string> productInternalMemorySize, List<string> productMobileRamSize)
        {
            try
            {
                var filterBuilder = Builders<Product>.Filter;
                var filterDefinitions = new List<FilterDefinition<Product>>();

                var productNameFilter = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression("xiaomi", "i"));
                filterDefinitions.Add(productNameFilter);

                // Fiyat aralığı filtresi
                if (productPrice != null && productPrice.Count >= 2)
                {
                    var priceRangeFilters = new List<FilterDefinition<Product>>();

                    // price listesi ikişerli olarak işlenir
                    for (int i = 0; i < productPrice.Count; i += 2)
                    {
                        decimal minPrice = productPrice[i];
                        decimal maxPrice = productPrice[i + 1];

                        var priceFilter = filterBuilder.And(
                            filterBuilder.Gte(x => x.ProductPrice, minPrice), // Minimum fiyat
                            filterBuilder.Lte(x => x.ProductPrice, maxPrice)  // Maksimum fiyat
                        );

                        priceRangeFilters.Add(priceFilter);
                    }

                    // Fiyat aralığı filtrelerini birleştir (herhangi biri geçerli ise)
                    if (priceRangeFilters.Count > 0)
                    {
                        var priceFiltersCombined = filterBuilder.Or(priceRangeFilters);
                        filterDefinitions.Add(priceFiltersCombined);
                    }
                }

                // Keyword filtresi
                if (productName != null && productName.Count > 0)
                {
                    var keywordFilters = new List<FilterDefinition<Product>>();

                    foreach (var product_name in productName)
                    {
                        var regexFilterProductName = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression(product_name, "i"));
                        keywordFilters.Add(regexFilterProductName);
                    }

                    // Keyword filtrelerini birleştir (herhangi biri geçerli ise)
                    if (keywordFilters.Count > 0)
                    {
                        var keywordFiltersCombined = filterBuilder.Or(keywordFilters);
                        filterDefinitions.Add(keywordFiltersCombined);
                    }
                }

                if (productModel != null && productModel.Count > 0)
                {
                    var keywordFilters = new List<FilterDefinition<Product>>();

                    foreach (var product_model in productModel)
                    {
                        var regexFilterProductModel = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression(product_model, "i"));
                        keywordFilters.Add(regexFilterProductModel);
                    }

                    if (keywordFilters.Count > 0)
                    {
                        var keywordFiltersCombined = filterBuilder.Or(keywordFilters);
                        filterDefinitions.Add(keywordFiltersCombined);
                    }

                }

                if (productInternalMemorySize != null && productInternalMemorySize.Count > 0)
                {
                    var keywordFilters = new List<FilterDefinition<Product>>();

                    foreach (var product_ınternal_memory_size in productInternalMemorySize)
                    {
                        var regexFilterProductModel = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression(product_ınternal_memory_size, "i"));
                        keywordFilters.Add(regexFilterProductModel);
                    }

                    if (keywordFilters.Count > 0)
                    {
                        var keywordFiltersCombined = filterBuilder.Or(keywordFilters);
                        filterDefinitions.Add(keywordFiltersCombined);
                    }

                }

                if (productMobileRamSize != null && productMobileRamSize.Count > 0)
                {
                    var keywordFilters = new List<FilterDefinition<Product>>();

                    foreach (var product_mobile_ram_size in productMobileRamSize)
                    {
                        var regexFilterProductModel = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression(product_mobile_ram_size, "i"));
                        keywordFilters.Add(regexFilterProductModel);
                    }

                    if (keywordFilters.Count > 0)
                    {
                        var keywordFiltersCombined = filterBuilder.Or(keywordFilters);
                        filterDefinitions.Add(keywordFiltersCombined);
                    }

                }

                // Tüm filtreleri birleştir (herhangi biri geçerli ise)
                var combinedFilter = filterBuilder.And(filterDefinitions);

                // Ürünleri filtreleyerek çek
                var products = await productCollection.Find(combinedFilter).ToListAsync();

                // Her bir ürün için kategori bilgisini al
                foreach (var product in products)
                {
                    product.Category = await categoryCollection.Find(c => c.CategoryID == product.CategoryID)
                                                                .SingleOrDefaultAsync();
                }

                // DTO'ya dönüştürme
                var resultDtoList = mapper.Map<List<ResultProductXiaomiPhoneDto>>(products);

                return resultDtoList;
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                Console.WriteLine($"Exception while getting filtered products: {ex.Message}");
                return null; // veya hata durumunu nasıl yönetmek istiyorsanız ona göre bir değer döndürebilirsiniz
            }
        }

        public async Task<List<ResultProductRealmePhoneDto>> GetProductRealmePhoneListAsync()
        {
            var filter = Builders<Product>.Filter.Regex(x => x.ProductName, new BsonRegularExpression("realme", "i"));
            var values = await productCollection.Find(filter).ToListAsync();           

            foreach (var item in values)
            {
                item.Category = await categoryCollection.Find(x => x.CategoryID == item.CategoryID).SingleOrDefaultAsync();
            }

            return mapper.Map<List<ResultProductRealmePhoneDto>>(values);
        }

        public async Task<List<ResultProductRealmePhoneDto>> GetProductRealmePhoneListFilterAsync(List<string> productName, List<decimal> productPrice, List<string> productModel, List<string> productInternalMemorySize, List<string> productMobileRamSize)
        {
            try
            {
                var filterBuilder = Builders<Product>.Filter;
                var filterDefinitions = new List<FilterDefinition<Product>>();

                var productNameFilter = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression("realme", "i"));
                filterDefinitions.Add(productNameFilter);

                // Fiyat aralığı filtresi
                if (productPrice != null && productPrice.Count >= 2)
                {
                    var priceRangeFilters = new List<FilterDefinition<Product>>();

                    // price listesi ikişerli olarak işlenir
                    for (int i = 0; i < productPrice.Count; i += 2)
                    {
                        decimal minPrice = productPrice[i];
                        decimal maxPrice = productPrice[i + 1];

                        var priceFilter = filterBuilder.And(
                            filterBuilder.Gte(x => x.ProductPrice, minPrice), // Minimum fiyat
                            filterBuilder.Lte(x => x.ProductPrice, maxPrice)  // Maksimum fiyat
                        );

                        priceRangeFilters.Add(priceFilter);
                    }

                    // Fiyat aralığı filtrelerini birleştir (herhangi biri geçerli ise)
                    if (priceRangeFilters.Count > 0)
                    {
                        var priceFiltersCombined = filterBuilder.Or(priceRangeFilters);
                        filterDefinitions.Add(priceFiltersCombined);
                    }
                }

                // Keyword filtresi
                if (productName != null && productName.Count > 0)
                {
                    var keywordFilters = new List<FilterDefinition<Product>>();

                    foreach (var product_name in productName)
                    {
                        var regexFilterProductName = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression(product_name, "i"));
                        keywordFilters.Add(regexFilterProductName);
                    }

                    // Keyword filtrelerini birleştir (herhangi biri geçerli ise)
                    if (keywordFilters.Count > 0)
                    {
                        var keywordFiltersCombined = filterBuilder.Or(keywordFilters);
                        filterDefinitions.Add(keywordFiltersCombined);
                    }
                }

                if (productModel != null && productModel.Count > 0)
                {
                    var keywordFilters = new List<FilterDefinition<Product>>();

                    foreach (var product_model in productModel)
                    {
                        var regexFilterProductModel = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression(product_model, "i"));
                        keywordFilters.Add(regexFilterProductModel);
                    }

                    if (keywordFilters.Count > 0)
                    {
                        var keywordFiltersCombined = filterBuilder.Or(keywordFilters);
                        filterDefinitions.Add(keywordFiltersCombined);
                    }

                }

                if (productInternalMemorySize != null && productInternalMemorySize.Count > 0)
                {
                    var keywordFilters = new List<FilterDefinition<Product>>();

                    foreach (var product_ınternal_memory_size in productInternalMemorySize)
                    {
                        var regexFilterProductModel = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression(product_ınternal_memory_size, "i"));
                        keywordFilters.Add(regexFilterProductModel);
                    }

                    if (keywordFilters.Count > 0)
                    {
                        var keywordFiltersCombined = filterBuilder.Or(keywordFilters);
                        filterDefinitions.Add(keywordFiltersCombined);
                    }

                }

                if (productMobileRamSize != null && productMobileRamSize.Count > 0)
                {
                    var keywordFilters = new List<FilterDefinition<Product>>();

                    foreach (var product_mobile_ram_size in productMobileRamSize)
                    {
                        var regexFilterProductModel = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression(product_mobile_ram_size, "i"));
                        keywordFilters.Add(regexFilterProductModel);
                    }

                    if (keywordFilters.Count > 0)
                    {
                        var keywordFiltersCombined = filterBuilder.Or(keywordFilters);
                        filterDefinitions.Add(keywordFiltersCombined);
                    }

                }

                // Tüm filtreleri birleştir (herhangi biri geçerli ise)
                var combinedFilter = filterBuilder.And(filterDefinitions);

                // Ürünleri filtreleyerek çek
                var products = await productCollection.Find(combinedFilter).ToListAsync();

                // Her bir ürün için kategori bilgisini al
                foreach (var product in products)
                {
                    product.Category = await categoryCollection.Find(c => c.CategoryID == product.CategoryID)
                                                                .SingleOrDefaultAsync();
                }

                // DTO'ya dönüştürme
                var resultDtoList = mapper.Map<List<ResultProductRealmePhoneDto>>(products);

                return resultDtoList;
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                Console.WriteLine($"Exception while getting filtered products: {ex.Message}");
                return null; // veya hata durumunu nasıl yönetmek istiyorsanız ona göre bir değer döndürebilirsiniz
            }
        }

        public async Task<List<ResultProductVivoPhoneDto>> GetProductVivoPhoneListAsync()
        {
            var filter = Builders<Product>.Filter.Regex(x => x.ProductName, new BsonRegularExpression("vivo", "i"));
            var values = await productCollection.Find(filter).ToListAsync();

            foreach (var item in values)
            {
                item.Category = await categoryCollection.Find(x => x.CategoryID == item.CategoryID).SingleOrDefaultAsync();
            }

            return mapper.Map<List<ResultProductVivoPhoneDto>>(values);
        }

        public async Task<List<ResultProductVivoPhoneDto>> GetProductVivoPhoneListFilterAsync(List<string> productName, List<decimal> productPrice, List<string> productModel, List<string> productInternalMemorySize, List<string> productMobileRamSize)
        {
            try
            {
                var filterBuilder = Builders<Product>.Filter;
                var filterDefinitions = new List<FilterDefinition<Product>>();

                var productNameFilter = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression("vivo", "i"));
                filterDefinitions.Add(productNameFilter);

                // Fiyat aralığı filtresi
                if (productPrice != null && productPrice.Count >= 2)
                {
                    var priceRangeFilters = new List<FilterDefinition<Product>>();

                    // price listesi ikişerli olarak işlenir
                    for (int i = 0; i < productPrice.Count; i += 2)
                    {
                        decimal minPrice = productPrice[i];
                        decimal maxPrice = productPrice[i + 1];

                        var priceFilter = filterBuilder.And(
                            filterBuilder.Gte(x => x.ProductPrice, minPrice), // Minimum fiyat
                            filterBuilder.Lte(x => x.ProductPrice, maxPrice)  // Maksimum fiyat
                        );

                        priceRangeFilters.Add(priceFilter);
                    }

                    // Fiyat aralığı filtrelerini birleştir (herhangi biri geçerli ise)
                    if (priceRangeFilters.Count > 0)
                    {
                        var priceFiltersCombined = filterBuilder.Or(priceRangeFilters);
                        filterDefinitions.Add(priceFiltersCombined);
                    }
                }

                // Keyword filtresi
                if (productName != null && productName.Count > 0)
                {
                    var keywordFilters = new List<FilterDefinition<Product>>();

                    foreach (var product_name in productName)
                    {
                        var regexFilterProductName = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression(product_name, "i"));
                        keywordFilters.Add(regexFilterProductName);
                    }

                    // Keyword filtrelerini birleştir (herhangi biri geçerli ise)
                    if (keywordFilters.Count > 0)
                    {
                        var keywordFiltersCombined = filterBuilder.Or(keywordFilters);
                        filterDefinitions.Add(keywordFiltersCombined);
                    }
                }

                if (productModel != null && productModel.Count > 0)
                {
                    var keywordFilters = new List<FilterDefinition<Product>>();

                    foreach (var product_model in productModel)
                    {
                        var regexFilterProductModel = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression(product_model, "i"));
                        keywordFilters.Add(regexFilterProductModel);
                    }

                    if (keywordFilters.Count > 0)
                    {
                        var keywordFiltersCombined = filterBuilder.Or(keywordFilters);
                        filterDefinitions.Add(keywordFiltersCombined);
                    }

                }

                if (productInternalMemorySize != null && productInternalMemorySize.Count > 0)
                {
                    var keywordFilters = new List<FilterDefinition<Product>>();

                    foreach (var product_ınternal_memory_size in productInternalMemorySize)
                    {
                        var regexFilterProductModel = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression(product_ınternal_memory_size, "i"));
                        keywordFilters.Add(regexFilterProductModel);
                    }

                    if (keywordFilters.Count > 0)
                    {
                        var keywordFiltersCombined = filterBuilder.Or(keywordFilters);
                        filterDefinitions.Add(keywordFiltersCombined);
                    }

                }

                if (productMobileRamSize != null && productMobileRamSize.Count > 0)
                {
                    var keywordFilters = new List<FilterDefinition<Product>>();

                    foreach (var product_mobile_ram_size in productMobileRamSize)
                    {
                        var regexFilterProductModel = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression(product_mobile_ram_size, "i"));
                        keywordFilters.Add(regexFilterProductModel);
                    }

                    if (keywordFilters.Count > 0)
                    {
                        var keywordFiltersCombined = filterBuilder.Or(keywordFilters);
                        filterDefinitions.Add(keywordFiltersCombined);
                    }

                }

                // Tüm filtreleri birleştir (herhangi biri geçerli ise)
                var combinedFilter = filterBuilder.And(filterDefinitions);

                // Ürünleri filtreleyerek çek
                var products = await productCollection.Find(combinedFilter).ToListAsync();

                // Her bir ürün için kategori bilgisini al
                foreach (var product in products)
                {
                    product.Category = await categoryCollection.Find(c => c.CategoryID == product.CategoryID)
                                                                .SingleOrDefaultAsync();
                }

                // DTO'ya dönüştürme
                var resultDtoList = mapper.Map<List<ResultProductVivoPhoneDto>>(products);

                return resultDtoList;
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                Console.WriteLine($"Exception while getting filtered products: {ex.Message}");
                return null; // veya hata durumunu nasıl yönetmek istiyorsanız ona göre bir değer döndürebilirsiniz
            }
        }

        public async Task<List<ResultProductTecnoCamonPhoneDto>> GetProductTecnoCamonPhoneListAsync()
        {
            var filter = Builders<Product>.Filter.Regex(x => x.ProductName, new BsonRegularExpression("tecno camon", "i"));
            var values = await productCollection.Find(filter).ToListAsync();

            foreach (var item in values)
            {
                item.Category = await categoryCollection.Find(x => x.CategoryID == item.CategoryID).SingleOrDefaultAsync();
            }

            return mapper.Map<List<ResultProductTecnoCamonPhoneDto>>(values);
        }

        public async Task<List<ResultProductTecnoCamonPhoneDto>> GetProductTecnoCamonPhoneListFilterAsync(List<string> productName, List<decimal> productPrice, List<string> productModel, List<string> productInternalMemorySize, List<string> productMobileRamSize)
        {
            try
            {
                var filterBuilder = Builders<Product>.Filter;
                var filterDefinitions = new List<FilterDefinition<Product>>();

                var productNameFilter = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression("tecno camon", "i"));
                filterDefinitions.Add(productNameFilter);

                // Fiyat aralığı filtresi
                if (productPrice != null && productPrice.Count >= 2)
                {
                    var priceRangeFilters = new List<FilterDefinition<Product>>();

                    // price listesi ikişerli olarak işlenir
                    for (int i = 0; i < productPrice.Count; i += 2)
                    {
                        decimal minPrice = productPrice[i];
                        decimal maxPrice = productPrice[i + 1];

                        var priceFilter = filterBuilder.And(
                            filterBuilder.Gte(x => x.ProductPrice, minPrice), // Minimum fiyat
                            filterBuilder.Lte(x => x.ProductPrice, maxPrice)  // Maksimum fiyat
                        );

                        priceRangeFilters.Add(priceFilter);
                    }

                    // Fiyat aralığı filtrelerini birleştir (herhangi biri geçerli ise)
                    if (priceRangeFilters.Count > 0)
                    {
                        var priceFiltersCombined = filterBuilder.Or(priceRangeFilters);
                        filterDefinitions.Add(priceFiltersCombined);
                    }
                }

                // Keyword filtresi
                if (productName != null && productName.Count > 0)
                {
                    var keywordFilters = new List<FilterDefinition<Product>>();

                    foreach (var product_name in productName)
                    {
                        var regexFilterProductName = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression(product_name, "i"));
                        keywordFilters.Add(regexFilterProductName);
                    }

                    // Keyword filtrelerini birleştir (herhangi biri geçerli ise)
                    if (keywordFilters.Count > 0)
                    {
                        var keywordFiltersCombined = filterBuilder.Or(keywordFilters);
                        filterDefinitions.Add(keywordFiltersCombined);
                    }
                }

                if (productModel != null && productModel.Count > 0)
                {
                    var keywordFilters = new List<FilterDefinition<Product>>();

                    foreach (var product_model in productModel)
                    {
                        var regexFilterProductModel = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression(product_model, "i"));
                        keywordFilters.Add(regexFilterProductModel);
                    }

                    if (keywordFilters.Count > 0)
                    {
                        var keywordFiltersCombined = filterBuilder.Or(keywordFilters);
                        filterDefinitions.Add(keywordFiltersCombined);
                    }

                }

                if (productInternalMemorySize != null && productInternalMemorySize.Count > 0)
                {
                    var keywordFilters = new List<FilterDefinition<Product>>();

                    foreach (var product_ınternal_memory_size in productInternalMemorySize)
                    {
                        var regexFilterProductModel = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression(product_ınternal_memory_size, "i"));
                        keywordFilters.Add(regexFilterProductModel);
                    }

                    if (keywordFilters.Count > 0)
                    {
                        var keywordFiltersCombined = filterBuilder.Or(keywordFilters);
                        filterDefinitions.Add(keywordFiltersCombined);
                    }

                }

                if (productMobileRamSize != null && productMobileRamSize.Count > 0)
                {
                    var keywordFilters = new List<FilterDefinition<Product>>();

                    foreach (var product_mobile_ram_size in productMobileRamSize)
                    {
                        var regexFilterProductModel = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression(product_mobile_ram_size, "i"));
                        keywordFilters.Add(regexFilterProductModel);
                    }

                    if (keywordFilters.Count > 0)
                    {
                        var keywordFiltersCombined = filterBuilder.Or(keywordFilters);
                        filterDefinitions.Add(keywordFiltersCombined);
                    }

                }

                // Tüm filtreleri birleştir (herhangi biri geçerli ise)
                var combinedFilter = filterBuilder.And(filterDefinitions);

                // Ürünleri filtreleyerek çek
                var products = await productCollection.Find(combinedFilter).ToListAsync();

                // Her bir ürün için kategori bilgisini al
                foreach (var product in products)
                {
                    product.Category = await categoryCollection.Find(c => c.CategoryID == product.CategoryID)
                                                                .SingleOrDefaultAsync();
                }

                // DTO'ya dönüştürme
                var resultDtoList = mapper.Map<List<ResultProductTecnoCamonPhoneDto>>(products);

                return resultDtoList;
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                Console.WriteLine($"Exception while getting filtered products: {ex.Message}");
                return null; // veya hata durumunu nasıl yönetmek istiyorsanız ona göre bir değer döndürebilirsiniz
            }
        }

        public async Task<List<ResultProductSmartWatchDto>> GetProductSmartWatchListAsync()
        {
            var filter = Builders<Product>.Filter.Regex(x => x.ProductName, new BsonRegularExpression("Watch", "i"));
            var values = await productCollection.Find(filter).ToListAsync();

            foreach (var item in values)
            {
                item.Category = await categoryCollection.Find(x => x.CategoryID == item.CategoryID).SingleOrDefaultAsync();
            }

            return mapper.Map<List<ResultProductSmartWatchDto>>(values);
        }

        public async Task<List<ResultProductSmartWatchDto>> GetProductSmartWatchListFilterAsync(List<string> productName, List<decimal> productPrice, List<string> productColor)
        {
            try
            {
                var filterBuilder = Builders<Product>.Filter;
                var filterDefinitions = new List<FilterDefinition<Product>>();

                var productNameFilter = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression("Watch", "i"));
                filterDefinitions.Add(productNameFilter);

                // Fiyat aralığı filtresi
                if (productPrice != null && productPrice.Count >= 2)
                {
                    var priceRangeFilters = new List<FilterDefinition<Product>>();

                    // price listesi ikişerli olarak işlenir
                    for (int i = 0; i < productPrice.Count; i += 2)
                    {
                        decimal minPrice = productPrice[i];
                        decimal maxPrice = productPrice[i + 1];

                        var priceFilter = filterBuilder.And(
                            filterBuilder.Gte(x => x.ProductPrice, minPrice), // Minimum fiyat
                            filterBuilder.Lte(x => x.ProductPrice, maxPrice)  // Maksimum fiyat
                        );

                        priceRangeFilters.Add(priceFilter);
                    }

                    // Fiyat aralığı filtrelerini birleştir (herhangi biri geçerli ise)
                    if (priceRangeFilters.Count > 0)
                    {
                        var priceFiltersCombined = filterBuilder.Or(priceRangeFilters);
                        filterDefinitions.Add(priceFiltersCombined);
                    }
                }

                // Keyword filtresi
                if (productName != null && productName.Count > 0)
                {
                    var keywordFilters = new List<FilterDefinition<Product>>();

                    foreach (var product_name in productName)
                    {
                        var regexFilterProductName = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression(product_name, "i"));
                        keywordFilters.Add(regexFilterProductName);
                    }

                    // Keyword filtrelerini birleştir (herhangi biri geçerli ise)
                    if (keywordFilters.Count > 0)
                    {
                        var keywordFiltersCombined = filterBuilder.Or(keywordFilters);
                        filterDefinitions.Add(keywordFiltersCombined);
                    }
                }

                if (productColor != null && productColor.Count > 0)
                {
                    var keywordFilters = new List<FilterDefinition<Product>>();

                    foreach (var product_color in productColor)
                    {
                        var regexFilterProductModel = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression(product_color, "i"));
                        keywordFilters.Add(regexFilterProductModel);
                    }

                    if (keywordFilters.Count > 0)
                    {
                        var keywordFiltersCombined = filterBuilder.Or(keywordFilters);
                        filterDefinitions.Add(keywordFiltersCombined);
                    }

                }

                

                // Tüm filtreleri birleştir (herhangi biri geçerli ise)
                var combinedFilter = filterBuilder.And(filterDefinitions);

                // Ürünleri filtreleyerek çek
                var products = await productCollection.Find(combinedFilter).ToListAsync();

                // Her bir ürün için kategori bilgisini al
                foreach (var product in products)
                {
                    product.Category = await categoryCollection.Find(c => c.CategoryID == product.CategoryID)
                                                                .SingleOrDefaultAsync();
                }

                // DTO'ya dönüştürme
                var resultDtoList = mapper.Map<List<ResultProductSmartWatchDto>>(products);

                return resultDtoList;
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                Console.WriteLine($"Exception while getting filtered products: {ex.Message}");
                return null; // veya hata durumunu nasıl yönetmek istiyorsanız ona göre bir değer döndürebilirsiniz
            }
        }

        public async Task<List<ResultProductMemoryCardDto>> GetProductMemoryCardListAsync()
        {
            var filter = Builders<Product>.Filter.Regex(x => x.ProductName, new BsonRegularExpression("hafıza kartı", "i"));
            var values = await productCollection.Find(filter).ToListAsync();

            foreach (var item in values)
            {
                item.Category = await categoryCollection.Find(x => x.CategoryID == item.CategoryID).SingleOrDefaultAsync();
            }

            return mapper.Map<List<ResultProductMemoryCardDto>>(values);
        }

        public async Task<List<ResultProductMemoryCardDto>> GetProductMemoryCardListFilterAsync(List<string> productName, List<decimal> productPrice, List<string> productModel, List<string> productStorage)
        {
            try
            {
                var filterBuilder = Builders<Product>.Filter;
                var filterDefinitions = new List<FilterDefinition<Product>>();

                var productNameFilter = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression("hafıza kartı", "i"));
                filterDefinitions.Add(productNameFilter);

                // Fiyat aralığı filtresi
                if (productPrice != null && productPrice.Count >= 2)
                {
                    var priceRangeFilters = new List<FilterDefinition<Product>>();

                    // price listesi ikişerli olarak işlenir
                    for (int i = 0; i < productPrice.Count; i += 2)
                    {
                        decimal minPrice = productPrice[i];
                        decimal maxPrice = productPrice[i + 1];

                        var priceFilter = filterBuilder.And(
                            filterBuilder.Gte(x => x.ProductPrice, minPrice), // Minimum fiyat
                            filterBuilder.Lte(x => x.ProductPrice, maxPrice)  // Maksimum fiyat
                        );

                        priceRangeFilters.Add(priceFilter);
                    }

                    // Fiyat aralığı filtrelerini birleştir (herhangi biri geçerli ise)
                    if (priceRangeFilters.Count > 0)
                    {
                        var priceFiltersCombined = filterBuilder.Or(priceRangeFilters);
                        filterDefinitions.Add(priceFiltersCombined);
                    }
                }

                // Keyword filtresi
                if (productName != null && productName.Count > 0)
                {
                    var keywordFilters = new List<FilterDefinition<Product>>();

                    foreach (var product_name in productName)
                    {
                        var regexFilterProductName = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression(product_name, "i"));
                        keywordFilters.Add(regexFilterProductName);
                    }

                    // Keyword filtrelerini birleştir (herhangi biri geçerli ise)
                    if (keywordFilters.Count > 0)
                    {
                        var keywordFiltersCombined = filterBuilder.Or(keywordFilters);
                        filterDefinitions.Add(keywordFiltersCombined);
                    }
                }

                if (productModel != null && productModel.Count > 0)
                {
                    var keywordFilters = new List<FilterDefinition<Product>>();

                    foreach (var product_model in productModel)
                    {
                        var regexFilterProductModel = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression(product_model, "i"));
                        keywordFilters.Add(regexFilterProductModel);
                    }

                    if (keywordFilters.Count > 0)
                    {
                        var keywordFiltersCombined = filterBuilder.Or(keywordFilters);
                        filterDefinitions.Add(keywordFiltersCombined);
                    }

                }

                if (productStorage != null && productStorage.Count > 0)
                {
                    var keywordFilters = new List<FilterDefinition<Product>>();

                    foreach (var product_storage in productStorage)
                    {
                        var regexFilterProductModel = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression(product_storage, "i"));
                        keywordFilters.Add(regexFilterProductModel);
                    }

                    if (keywordFilters.Count > 0)
                    {
                        var keywordFiltersCombined = filterBuilder.Or(keywordFilters);
                        filterDefinitions.Add(keywordFiltersCombined);
                    }

                }


                // Tüm filtreleri birleştir (herhangi biri geçerli ise)
                var combinedFilter = filterBuilder.And(filterDefinitions);

                // Ürünleri filtreleyerek çek
                var products = await productCollection.Find(combinedFilter).ToListAsync();

                // Her bir ürün için kategori bilgisini al
                foreach (var product in products)
                {
                    product.Category = await categoryCollection.Find(c => c.CategoryID == product.CategoryID)
                                                                .SingleOrDefaultAsync();
                }

                // DTO'ya dönüştürme
                var resultDtoList = mapper.Map<List<ResultProductMemoryCardDto>>(products);

                return resultDtoList;
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                Console.WriteLine($"Exception while getting filtered products: {ex.Message}");
                return null; // veya hata durumunu nasıl yönetmek istiyorsanız ona göre bir değer döndürebilirsiniz
            }
        }

        public async Task<List<ResultProductLaptopComputerDto>> GetProductLaptopComputerListAsync()
        {
            var filter = Builders<Product>.Filter.Regex(x => x.ProductName, new BsonRegularExpression("Dizüstü", "i"));
            var values = await productCollection.Find(filter).ToListAsync();

            foreach (var item in values)
            {
                item.Category = await categoryCollection.Find(x => x.CategoryID == item.CategoryID).SingleOrDefaultAsync();
            }

            return mapper.Map<List<ResultProductLaptopComputerDto>>(values);
        }

        public async Task<List<ResultProductLaptopComputerDto>> GetProductLaptopComputerListFilterAsync(List<string> productName, List<decimal> productPrice, List<string> productProcessModel, List<string> productGrapichCardModel, List<string> productMemoryRam)
        {
            try
            {
                var filterBuilder = Builders<Product>.Filter;
                var filterDefinitions = new List<FilterDefinition<Product>>();

                var productNameFilter = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression("Dizüstü Bilgisayar", "i"));
                filterDefinitions.Add(productNameFilter);

                // Fiyat aralığı filtresi
                if (productPrice != null && productPrice.Count >= 2)
                {
                    var priceRangeFilters = new List<FilterDefinition<Product>>();

                    // price listesi ikişerli olarak işlenir
                    for (int i = 0; i < productPrice.Count; i += 2)
                    {
                        decimal minPrice = productPrice[i];
                        decimal maxPrice = productPrice[i + 1];

                        var priceFilter = filterBuilder.And(
                            filterBuilder.Gte(x => x.ProductPrice, minPrice), // Minimum fiyat
                            filterBuilder.Lte(x => x.ProductPrice, maxPrice)  // Maksimum fiyat
                        );

                        priceRangeFilters.Add(priceFilter);
                    }

                    // Fiyat aralığı filtrelerini birleştir (herhangi biri geçerli ise)
                    if (priceRangeFilters.Count > 0)
                    {
                        var priceFiltersCombined = filterBuilder.Or(priceRangeFilters);
                        filterDefinitions.Add(priceFiltersCombined);
                    }
                }

                // Keyword filtresi
                if (productName != null && productName.Count > 0)
                {
                    var keywordFilters = new List<FilterDefinition<Product>>();

                    foreach (var product_name in productName)
                    {
                        var regexFilterProductName = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression(product_name, "i"));
                        keywordFilters.Add(regexFilterProductName);
                    }

                    // Keyword filtrelerini birleştir (herhangi biri geçerli ise)
                    if (keywordFilters.Count > 0)
                    {
                        var keywordFiltersCombined = filterBuilder.Or(keywordFilters);
                        filterDefinitions.Add(keywordFiltersCombined);
                    }
                }

                if (productProcessModel != null && productProcessModel.Count > 0)
                {
                    var keywordFilters = new List<FilterDefinition<Product>>();

                    foreach (var product_process_model in productProcessModel)
                    {
                        var regexFilterProductModel = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression(product_process_model, "i"));
                        keywordFilters.Add(regexFilterProductModel);
                    }

                    if (keywordFilters.Count > 0)
                    {
                        var keywordFiltersCombined = filterBuilder.Or(keywordFilters);
                        filterDefinitions.Add(keywordFiltersCombined);
                    }

                }

                if (productGrapichCardModel != null && productGrapichCardModel.Count > 0)
                {
                    var keywordFilters = new List<FilterDefinition<Product>>();

                    foreach (var product_grapich_card in productGrapichCardModel)
                    {
                        var regexFilterProductModel = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression(product_grapich_card, "i"));
                        keywordFilters.Add(regexFilterProductModel);
                    }

                    if (keywordFilters.Count > 0)
                    {
                        var keywordFiltersCombined = filterBuilder.Or(keywordFilters);
                        filterDefinitions.Add(keywordFiltersCombined);
                    }

                }

                if (productMemoryRam != null && productMemoryRam.Count > 0)
                {
                    var keywordFilters = new List<FilterDefinition<Product>>();

                    foreach (var product_memory_ram in productMemoryRam)
                    {
                        var regexFilterProductModel = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression(product_memory_ram, "i"));
                        keywordFilters.Add(regexFilterProductModel);
                    }

                    if (keywordFilters.Count > 0)
                    {
                        var keywordFiltersCombined = filterBuilder.Or(keywordFilters);
                        filterDefinitions.Add(keywordFiltersCombined);
                    }

                }


                // Tüm filtreleri birleştir (herhangi biri geçerli ise)
                var combinedFilter = filterBuilder.And(filterDefinitions);

                // Ürünleri filtreleyerek çek
                var products = await productCollection.Find(combinedFilter).ToListAsync();

                // Her bir ürün için kategori bilgisini al
                foreach (var product in products)
                {
                    product.Category = await categoryCollection.Find(c => c.CategoryID == product.CategoryID)
                                                                .SingleOrDefaultAsync();
                }

                // DTO'ya dönüştürme
                var resultDtoList = mapper.Map<List<ResultProductLaptopComputerDto>>(products);

                return resultDtoList;
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                Console.WriteLine($"Exception while getting filtered products: {ex.Message}");
                return null; // veya hata durumunu nasıl yönetmek istiyorsanız ona göre bir değer döndürebilirsiniz
            }
        }

        public async Task<List<ResultProductDesktopComputerDto>> GetProductDesktopComputerListAsync()
        {
            var filter = Builders<Product>.Filter.Regex(x => x.ProductName, new BsonRegularExpression("Masaüstü", "i"));
            var values = await productCollection.Find(filter).ToListAsync();

            foreach (var item in values)
            {
                item.Category = await categoryCollection.Find(x => x.CategoryID == item.CategoryID).SingleOrDefaultAsync();
            }

            return mapper.Map<List<ResultProductDesktopComputerDto>>(values);
        }

        public async Task<List<ResultProductDesktopComputerDto>> GetProductDesktopComputerListFilterAsync(List<string> productName, List<decimal> productPrice, List<string> productProcessModel, List<string> productGrapichCardModel, List<string> productMemoryRam)
        {
            try
            {
                var filterBuilder = Builders<Product>.Filter;
                var filterDefinitions = new List<FilterDefinition<Product>>();

                var productNameFilter = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression("Masaüstü", "i"));
                filterDefinitions.Add(productNameFilter);

                // Fiyat aralığı filtresi
                if (productPrice != null && productPrice.Count >= 2)
                {
                    var priceRangeFilters = new List<FilterDefinition<Product>>();

                    // price listesi ikişerli olarak işlenir
                    for (int i = 0; i < productPrice.Count; i += 2)
                    {
                        decimal minPrice = productPrice[i];
                        decimal maxPrice = productPrice[i + 1];

                        var priceFilter = filterBuilder.And(
                            filterBuilder.Gte(x => x.ProductPrice, minPrice), // Minimum fiyat
                            filterBuilder.Lte(x => x.ProductPrice, maxPrice)  // Maksimum fiyat
                        );

                        priceRangeFilters.Add(priceFilter);
                    }

                    // Fiyat aralığı filtrelerini birleştir (herhangi biri geçerli ise)
                    if (priceRangeFilters.Count > 0)
                    {
                        var priceFiltersCombined = filterBuilder.Or(priceRangeFilters);
                        filterDefinitions.Add(priceFiltersCombined);
                    }
                }

                // Keyword filtresi
                if (productName != null && productName.Count > 0)
                {
                    var keywordFilters = new List<FilterDefinition<Product>>();

                    foreach (var product_name in productName)
                    {
                        var regexFilterProductName = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression(product_name, "i"));
                        keywordFilters.Add(regexFilterProductName);
                    }

                    // Keyword filtrelerini birleştir (herhangi biri geçerli ise)
                    if (keywordFilters.Count > 0)
                    {
                        var keywordFiltersCombined = filterBuilder.Or(keywordFilters);
                        filterDefinitions.Add(keywordFiltersCombined);
                    }
                }

                if (productProcessModel != null && productProcessModel.Count > 0)
                {
                    var keywordFilters = new List<FilterDefinition<Product>>();

                    foreach (var product_process_model in productProcessModel)
                    {
                        var regexFilterProductModel = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression(product_process_model, "i"));
                        keywordFilters.Add(regexFilterProductModel);
                    }

                    if (keywordFilters.Count > 0)
                    {
                        var keywordFiltersCombined = filterBuilder.Or(keywordFilters);
                        filterDefinitions.Add(keywordFiltersCombined);
                    }

                }

                if (productGrapichCardModel != null && productGrapichCardModel.Count > 0)
                {
                    var keywordFilters = new List<FilterDefinition<Product>>();

                    foreach (var product_grapich_card in productGrapichCardModel)
                    {
                        var regexFilterProductModel = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression(product_grapich_card, "i"));
                        keywordFilters.Add(regexFilterProductModel);
                    }

                    if (keywordFilters.Count > 0)
                    {
                        var keywordFiltersCombined = filterBuilder.Or(keywordFilters);
                        filterDefinitions.Add(keywordFiltersCombined);
                    }

                }

                if (productMemoryRam != null && productMemoryRam.Count > 0)
                {
                    var keywordFilters = new List<FilterDefinition<Product>>();

                    foreach (var product_memory_ram in productMemoryRam)
                    {
                        var regexFilterProductModel = filterBuilder.Regex(x => x.ProductName, new BsonRegularExpression(product_memory_ram, "i"));
                        keywordFilters.Add(regexFilterProductModel);
                    }

                    if (keywordFilters.Count > 0)
                    {
                        var keywordFiltersCombined = filterBuilder.Or(keywordFilters);
                        filterDefinitions.Add(keywordFiltersCombined);
                    }

                }


                // Tüm filtreleri birleştir (herhangi biri geçerli ise)
                var combinedFilter = filterBuilder.And(filterDefinitions);

                // Ürünleri filtreleyerek çek
                var products = await productCollection.Find(combinedFilter).ToListAsync();

                // Her bir ürün için kategori bilgisini al
                foreach (var product in products)
                {
                    product.Category = await categoryCollection.Find(c => c.CategoryID == product.CategoryID)
                                                                .SingleOrDefaultAsync();
                }

                // DTO'ya dönüştürme
                var resultDtoList = mapper.Map<List<ResultProductDesktopComputerDto>>(products);

                return resultDtoList;
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                Console.WriteLine($"Exception while getting filtered products: {ex.Message}");
                return null; // veya hata durumunu nasıl yönetmek istiyorsanız ona göre bir değer döndürebilirsiniz
            }
        }       

        public async Task<List<ResultProductWithCategoryDto>> GetSearchProductAsync(string productCategory, string productTextSearch)
        {
            if (productCategory != "All")
            {        
                var builder = Builders<Product>.Filter;
                var filter = builder.Where(x => x.CategoryID == productCategory) & builder.Regex(x => x.ProductName, new BsonRegularExpression(productTextSearch, "i"));

                var values = await productCollection.Find(filter).ToListAsync();

                foreach (var item in values)
                {
                    item.Category = await categoryCollection.Find(x => x.CategoryID == item.CategoryID).SingleOrDefaultAsync();
                }

                return mapper.Map<List<ResultProductWithCategoryDto>>(values);
            }
            else
            {
                var builder_all = Builders<Product>.Filter;
                var filter_all = builder_all.Regex(x => x.ProductName, new BsonRegularExpression(productTextSearch, "i"));

                var values_all = await productCollection.Find(filter_all).ToListAsync();

                foreach (var item in values_all)
                {
                    item.Category = await categoryCollection.Find(x => x.CategoryID == item.CategoryID).SingleOrDefaultAsync();
                }

                return mapper.Map<List<ResultProductWithCategoryDto>>(values_all);
            }
           
        }
    }
}
