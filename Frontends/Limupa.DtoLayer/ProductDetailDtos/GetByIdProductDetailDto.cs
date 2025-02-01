using Limupa.DtoLayer.ProductDtos;

namespace Limupa.DtoLayer.ProductDetailDtos
{
    public class GetByIdProductDetailDto
    {
        public string ProductDetailID { get; set; }
        public string ProductDescription { get; set; }
        public string ProductInfo { get; set; }
        public string ProductUrlSeo { get; set; }
        public string ProductID { get; set; }
        public ResultProductDto Product { get; set; }

    }
}
