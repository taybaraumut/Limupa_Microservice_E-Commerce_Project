
namespace Limupa.Order.Application.Features.CQRS.Commands.AddressCommands
{
    public class CreateAddressCommand
    {
        public string UserID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string DetailOne { get; set; }
        public string DetailTwo { get; set; }
        public string Description { get; set; }
        public string ZipCode { get; set; }
    }
}
