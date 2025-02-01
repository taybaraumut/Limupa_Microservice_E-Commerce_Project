using Limupa.Catalog.Dtos.ProductDtos;

namespace Limupa.Catalog.Api.Dtos.ProductImageDtos
{
    public class ResultProductImageWithProductDto
    {
        public string ProductImageID { get; set; }
        public List<string> ProductBigImageUrl { get; set; }
        public List<string> ProductSmallImageUrl { get; set; }
        public ResultProductDto Product { get; set; }
    }
}
