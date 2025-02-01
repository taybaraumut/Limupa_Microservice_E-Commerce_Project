using AutoMapper;
using Limupa.Catalog.Api.Dtos.AboutDtos;
using Limupa.Catalog.Api.Entities;
using Limupa.Catalog.Settings;
using MongoDB.Driver;

namespace Limupa.Catalog.Api.Services.AboutServices
{
    public class AboutService : IAboutService
    {
        private readonly IMongoCollection<About> AboutCollection;
        private readonly IMapper mapper;

        public AboutService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            AboutCollection = database.GetCollection<About>(databaseSettings.AboutCollectionName);
            this.mapper = mapper;
        }

        public async Task CreateAboutAsync(CreateAboutDto createAboutDto)
        {
            var value = mapper.Map<About>(createAboutDto);
            await AboutCollection.InsertOneAsync(value);
        }

        public async Task DeleteAboutAsync(string id)
        {
            await AboutCollection.DeleteOneAsync(x => x.AboutID == id);
        }

        public async Task<List<ResultAboutDto>> GetAllAboutAsync()
        {
            var values = mapper.Map<List<ResultAboutDto>>(await AboutCollection.Find(x => true).ToListAsync());
            return values;
        }

        public async Task<GetByIdAboutDto> GetByIdAboutAsync(string id)
        {
            var values = await AboutCollection.Find<About>(x => x.AboutID == id).FirstOrDefaultAsync();
            return mapper.Map<GetByIdAboutDto>(values);
        }

        public async Task UpdateAboutAsync(UpdateAboutDto updateAboutDto)
        {
            var value = mapper.Map<About>(updateAboutDto);
            await AboutCollection.FindOneAndReplaceAsync(x => x.AboutID == updateAboutDto.AboutID, value);
        }
    }
}
