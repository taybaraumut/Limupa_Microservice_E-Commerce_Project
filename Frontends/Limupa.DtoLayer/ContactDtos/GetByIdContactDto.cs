namespace Limupa.DtoLayer.ContactDtos
{
    public class GetByIdContactDto
    {
        public string ContactID { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactSubject { get; set; }
        public string ContactMessage { get; set; }
        public bool ContactIsRead { get; set; }
        public DateTime ContactSendDate { get; set; }
    }
}
