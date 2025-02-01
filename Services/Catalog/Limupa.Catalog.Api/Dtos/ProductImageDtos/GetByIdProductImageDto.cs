
namespace Limupa.Catalog.Dtos.ProductImageDtos
{
    public class GetByIdProductImageDto
    {
        public string ProductImageID { get; set; }
        public List<string> ProductBigImageUrl { get; set; }
        public List<string> ProductSmallImageUrl { get; set; }
        public string ProductID { get; set; }
    }
}
