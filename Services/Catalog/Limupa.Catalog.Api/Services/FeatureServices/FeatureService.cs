using AutoMapper;
using Limupa.Catalog.Api.Dtos.FeatureDtos;
using Limupa.Catalog.Api.Entities;
using Limupa.Catalog.Settings;
using MongoDB.Driver;

namespace Limupa.Catalog.Api.Services.FeatureServices
{
    public class FeatureService : IFeatureService
    {
        private readonly IMongoCollection<Feature> FeatureCollection;
        private readonly IMapper mapper;

        public FeatureService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            FeatureCollection = database.GetCollection<Feature>(databaseSettings.FeatureCollectionName);
            this.mapper = mapper;
        }

        public async Task CreateFeatureAsync(CreateFeatureDto createFeatureDto)
        {
            var value = mapper.Map<Feature>(createFeatureDto);
            await FeatureCollection.InsertOneAsync(value);
        }

        public async Task DeleteFeatureAsync(string id)
        {
            await FeatureCollection.DeleteOneAsync(x => x.FeatureID == id);
        }

        public async Task<List<ResultFeatureDto>> GetAllFeatureAsync()
        {
            var values = mapper.Map<List<ResultFeatureDto>>(await FeatureCollection.Find(x => true).ToListAsync());
            return values;
        }

        public async Task<GetByIdFeatureDto> GetByIdFeatureAsync(string id)
        {
            var value = await FeatureCollection.Find<Feature>(x => x.FeatureID == id).FirstOrDefaultAsync();
            return mapper.Map<GetByIdFeatureDto>(value);
        }

        public async Task UpdateFeatureAsync(UpdateFeatureDto updateFeatureDto)
        {
            var value = mapper.Map<Feature>(updateFeatureDto);
            await FeatureCollection.FindOneAndReplaceAsync(x => x.FeatureID == updateFeatureDto.FeatureID, value);
        }
    }
}
