using AutoMapper;
using Limupa.Catalog.Dtos.CategoryDtos;
using Limupa.Catalog.Entities;
using Limupa.Catalog.Settings;
using MongoDB.Driver;

namespace Limupa.Catalog.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> categoryCollection;
        private readonly IMapper mapper;

        public CategoryService(IMapper mapper,IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            this.mapper = mapper;
        }

        public async Task CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            var value = mapper.Map<Category>(createCategoryDto);
            await categoryCollection.InsertOneAsync(value);
        }

        public async Task DeleteCategoryAsync(string id)
        {
            await categoryCollection.DeleteOneAsync(x=>x.CategoryID == id);
        }

        public async Task<List<ResultCategoryDto>> GetAllCategoryAsync()
        {
            var values = mapper.Map<List<ResultCategoryDto>>(await categoryCollection.Find(x=>true).ToListAsync());
            return values;
        }

        public async Task<GetByIdCategoryDto> GetByIdCategoryAsync(string id)
        {
            var values = await categoryCollection.Find<Category>(x => x.CategoryID == id).FirstOrDefaultAsync();
            return mapper.Map<GetByIdCategoryDto>(values);
        }

        public async Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
        {
            var value = mapper.Map<Category>(updateCategoryDto);
            await categoryCollection.FindOneAndReplaceAsync(x => x.CategoryID == updateCategoryDto.CategoryID, value);
        }
    }
}
