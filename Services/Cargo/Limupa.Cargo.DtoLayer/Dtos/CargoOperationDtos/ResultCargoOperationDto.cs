
namespace Limupa.Cargo.DtoLayer.Dtos.CargoOperationDtos
{
    public class ResultCargoOperationDto
    {
        public int CargoOperationID { get; set; }
        public string CargoBarcode { get; set; }
        public string CargoDescription { get; set; }
        public DateTime CargoOperationDate { get; set; }
    }
}
