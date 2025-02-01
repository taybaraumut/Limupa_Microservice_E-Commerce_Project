using Microsoft.AspNetCore.Http;

namespace Limupa.DtoLayer.ProductDtos
{
    public class CreateProductDto
    {
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductDescription { get; set; }
        public string ProductImageUrl { get; set; }
        public string CategoryID { get; set; }
        public IFormFile Photo { get; set; }
        public string? SavedUrl { get; set; }
        public string? SavedFileName { get; set; }
        public string? ProductUrlSeo { get; set; }
    }
}
