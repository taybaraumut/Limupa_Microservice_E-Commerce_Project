
namespace Limupa.Cargo.EntityLayer.Concrete
{
    public class CargoDetail
    {
        public int CargoDetailID { get; set; }
        public string CargoSenderCustomer { get; set; }
        public string CargoReceiverCustomer { get; set; }
        public int CargoBarcode { get; set; }
        public int CargoCompanyId { get; set; }
        public CargoCompany CargoCompany { get; set; }
    }
}
