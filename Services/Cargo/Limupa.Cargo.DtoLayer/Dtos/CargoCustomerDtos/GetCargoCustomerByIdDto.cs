﻿
namespace Limupa.Cargo.DtoLayer.Dtos.CargoCustomerDtos
{
    public class GetCargoCustomerByIdDto
    {
        public int CargoCustomerID { get; set; }
        public string CargoCustomerName { get; set; }
        public string CargoCustomerSurname { get; set; }
        public string CargoCustomerEmail { get; set; }
        public string CargoCustomerPhone { get; set; }
        public string CargoCustomerDistrict { get; set; }
        public string CargoCustomerCity { get; set; }
        public string CargoCustomerAddress { get; set; }
        public string? CargoCustomerUserID { get; set; }
    }
}
