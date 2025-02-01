using Limupa.Cargo.EntityLayer.Concrete;


namespace Limupa.Cargo.DtoLayer.Dtos.CargoDetailDtos
{
    public class GetCargoDetailByIdDto
    {
        public int CargoDetailID { get; set; }
        public string CargoSenderCustomer { get; set; }
        public string CargoReceiverCustomer { get; set; }
        public int CargoBarcode { get; set; }
        public int CargoCompanyId { get; set; }
        public CargoCompany CargoCompany { get; set; }
    }
}
