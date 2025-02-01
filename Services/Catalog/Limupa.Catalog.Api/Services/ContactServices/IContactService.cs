using Limupa.Catalog.Api.Dtos.ContactDtos;

namespace Limupa.Catalog.Api.Services.ContactServices
{
    public interface IContactService
    {
        Task<List<ResultContactDto>> GetAllContactAsync();
        Task CreateContactAsync(CreateContactDto createContactDto);
        Task UpdateContactAsync(UpdateContactDto updateContactDto);
        Task<GetByIdContactDto> GetByIdContactAsync(string id);
        Task DeleteContactAsync(string id);
    }
}
