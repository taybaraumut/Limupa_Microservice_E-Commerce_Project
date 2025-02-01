using AutoMapper;
using Limupa.Catalog.Api.Dtos.SpecialOfferDtos;
using Limupa.Catalog.Api.Entities;
using Limupa.Catalog.Settings;
using MongoDB.Driver;

namespace Limupa.Catalog.Api.Services.SpecialOfferServices
{
    public class SpecialOfferService : ISpecialOfferService
    {
        private readonly IMongoCollection<SpecialOffer> specialOfferCollection;
        private readonly IMapper mapper;

        public SpecialOfferService(IMapper mapper,IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            specialOfferCollection = database.GetCollection<SpecialOffer>(databaseSettings.SpecialOfferCollectionName);
            this.mapper = mapper;
        }

        public async Task CreateSpecialOfferAsync(CreateSpecialOfferDto createSpecialOfferDto)
        {
            var value = mapper.Map<SpecialOffer>(createSpecialOfferDto);
            await specialOfferCollection.InsertOneAsync(value);
        }

        public async Task DeleteSpecialOfferAsync(string id)
        {
           await specialOfferCollection.DeleteOneAsync(x=>x.SpecialOfferID==id);
        }

        public async Task<List<ResultSpecialOfferDto>> GetAllSpecialOfferAsync()
        {
            var values = mapper.Map<List<ResultSpecialOfferDto>>(await specialOfferCollection.Find(x=>true).ToListAsync());
            return values;
        }

        public async Task<GetByIdSpecialOfferDto> GetByIdSpecialOfferAsync(string id)
        {
            var value = mapper.Map<GetByIdSpecialOfferDto>(await specialOfferCollection.Find(x=>x.SpecialOfferID==id).FirstOrDefaultAsync());
            return value;
        }

        public async Task UpdateSpecialOfferAsync(UpdateSpecialOfferDto updateSpecialOfferDto)
        {
            var value = mapper.Map<SpecialOffer>(updateSpecialOfferDto);
            await specialOfferCollection.FindOneAndReplaceAsync<SpecialOffer>(x => x.SpecialOfferID == updateSpecialOfferDto.SpecialOfferID, value);
        }
    }
}
