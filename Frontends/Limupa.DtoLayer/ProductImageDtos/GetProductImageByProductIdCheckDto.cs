namespace Limupa.DtoLayer.ProductImageDtos
{
    public class GetProductImageByProductIdCheckDto
    {
        public string ProductImageID { get; set; }
        public List<string> ProductBigImageUrl { get; set; }
        public List<string> ProductSmallImageUrl { get; set; }
        public string ProductID { get; set; }
        public string ProductUrlSeo { get; set; }
    }
}
