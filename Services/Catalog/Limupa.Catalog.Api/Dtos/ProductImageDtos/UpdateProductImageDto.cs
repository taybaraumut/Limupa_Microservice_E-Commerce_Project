namespace Limupa.Catalog.Dtos.ProductImageDtos
{
    public class UpdateProductImageDto
    {
        public string ProductImageID { get; set; }
        public List<string> ProductBigImageUrl { get; set; }
        public List<string> ProductSmallImageUrl { get; set; }
        public string ProductID { get; set; }
        public List<IFormFile>? BigPhoto { get; set; }
        public List<IFormFile>? SmallPhoto { get; set; }
        public List<string>? BigSavedUrl { get; set; }
        public List<string>? BigSavedFileName { get; set; }
        public List<string>? SmallSavedUrl { get; set; }
        public List<string>? SmallSavedFileName { get; set; }
    }
}
