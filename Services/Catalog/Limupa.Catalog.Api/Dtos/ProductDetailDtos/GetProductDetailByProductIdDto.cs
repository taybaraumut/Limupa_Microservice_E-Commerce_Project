using Limupa.Catalog.Dtos.ProductDtos;

namespace Limupa.Catalog.Api.Dtos.ProductDetailDtos
{
    public class GetProductDetailByProductIdDto
    {
        public string ProductDetailID { get; set; }
        public string ProductDescription { get; set; }
        public string ProductInfo { get; set; }
        public string ProductUrlSeo { get; set; }
        public string ProductID { get; set; }
        public ResultProductDto Product { get; set; }
    }
}
