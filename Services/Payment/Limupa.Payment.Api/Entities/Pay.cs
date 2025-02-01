using System.ComponentModel.DataAnnotations;

namespace Limupa.Payment.Api.Entities
{
    public class Pay
    {
        [Key]
        public int PaymentID { get; set; }
        public string UserID { get; set; }
        public List<string> ProductName { get; set; }
        public List<string> ProductImageUrl { get; set; }
        public Guid PaymentTransactionID { get; set; }
        public decimal PaymentAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool PaymentStatus { get; set; }
    }
}
