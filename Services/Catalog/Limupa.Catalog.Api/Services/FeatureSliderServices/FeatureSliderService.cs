using AutoMapper;
using Limupa.Catalog.Api.Dtos.FeatureSliderDtos;
using Limupa.Catalog.Api.Entities;
using Limupa.Catalog.Settings;
using MongoDB.Driver;

namespace Limupa.Catalog.Api.Services.FeatureSliderServices
{
    public class FeatureSliderService : IFeatureSliderService
    {
        private readonly IMongoCollection<FeatureSlider> featureSliderCollection;
        private readonly IMapper mapper;

        public FeatureSliderService(IMapper mapper,IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            featureSliderCollection = database.GetCollection<FeatureSlider>(databaseSettings.FeatureSliderCollectionName);
            this.mapper = mapper;
        }

        public async Task CreateFeatureSliderAsync(CreateFeatureSliderDto createFeatureSliderDto)
        {
            var value = mapper.Map<FeatureSlider>(createFeatureSliderDto);
            await featureSliderCollection.InsertOneAsync(value);
        }

        public async Task DeleteFeatureSliderAsync(string id)
        {
            await featureSliderCollection.DeleteOneAsync(x=>x.FeatureSliderID==id);
        }

        public Task FeatureSliderChangeStatusToFalse(string id)
        {
            throw new NotImplementedException();
        }

        public Task FeatureSliderChangeStatusToTrue(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ResultFeatureSliderDto>> GetAllFeatureSliderAsync()
        {
            var values = mapper.Map<List<ResultFeatureSliderDto>>(await featureSliderCollection.Find(x=>true).ToListAsync());
            return values;
        }

        public async Task<GetByIdFeatureSliderDto> GetByIdFeatureSliderAsync(string id)
        {
            var value = await featureSliderCollection.Find<FeatureSlider>(x => x.FeatureSliderID == id).FirstOrDefaultAsync();
            return mapper.Map<GetByIdFeatureSliderDto>(value);
        }

        public async Task UpdateFeatureSliderAsync(UpdateFeatureSliderDto updateFeatureSliderDto)
        {
            var value = mapper.Map<FeatureSlider>(updateFeatureSliderDto);
            await featureSliderCollection.FindOneAndReplaceAsync(x=>x.FeatureSliderID==updateFeatureSliderDto.FeatureSliderID,value);
        }
    }
}
