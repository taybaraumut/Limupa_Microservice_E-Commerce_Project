using AutoMapper;
using Limupa.Catalog.Api.Dtos.ContactDtos;
using Limupa.Catalog.Api.Entities;
using Limupa.Catalog.Settings;
using MongoDB.Driver;

namespace Limupa.Catalog.Api.Services.ContactServices
{
    public class ContactService : IContactService
    {
        private readonly IMongoCollection<Contact> contactCollection;
        private readonly IMapper mapper;
        public ContactService(IDatabaseSettings databaseSettings, IMapper mapper)
        {
            var client = new MongoClient();
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            contactCollection = database.GetCollection<Contact>(databaseSettings.ContactCollectionName);

            this.mapper = mapper;
        }
        public async Task CreateContactAsync(CreateContactDto createContactDto)
        {   
            var value = mapper.Map<Contact>(createContactDto);
            await contactCollection.InsertOneAsync(value);
        }

        public async Task DeleteContactAsync(string id)
        {
            await contactCollection.DeleteOneAsync(x=>x.ContactID == id);
        }

        public async Task<List<ResultContactDto>> GetAllContactAsync()
        {
           
            var values = mapper.Map<List<ResultContactDto>>(await contactCollection.Find(x=>true).ToListAsync());
            return values;
        }

        public async Task<GetByIdContactDto> GetByIdContactAsync(string id)
        {
            var value = mapper.Map<GetByIdContactDto>(await contactCollection.Find(x=>x.ContactID == id).FirstOrDefaultAsync());
            return value;
        }

        public async Task UpdateContactAsync(UpdateContactDto updateContactDto)
        {
            var value = mapper.Map<Contact>(updateContactDto);

            await contactCollection.FindOneAndReplaceAsync<Contact>(x => x.ContactID == updateContactDto.ContactID,value);
        }
    }
}
