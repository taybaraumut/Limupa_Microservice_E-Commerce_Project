using Limupa.Catalog.Api.Dtos.ContactDtos;
using Limupa.Catalog.Api.Entities;
using Limupa.Catalog.Api.Services.ContactServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Limupa.Catalog.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService ContactService;

        public ContactsController(IContactService ContactService)
        {
            this.ContactService = ContactService;
        }
        [HttpGet]
        public async Task<IActionResult> ContactList()
        {
            var values = await ContactService.GetAllContactAsync();
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateContactDto createContactDto)
        {          
            await ContactService.CreateContactAsync(createContactDto);
            return Ok("Successful");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(string id)
        {
            await ContactService.DeleteContactAsync(id);
            return Ok("Successful");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateContact(UpdateContactDto updateContactDto)
        {
            await ContactService.UpdateContactAsync(updateContactDto);
            return Ok("Successful");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactById(string id)
        {
            var value = await ContactService.GetByIdContactAsync(id);
            return Ok(value);
        }
    }
}
