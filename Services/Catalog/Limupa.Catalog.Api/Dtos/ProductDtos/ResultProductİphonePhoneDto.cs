using Limupa.Catalog.Dtos.CategoryDtos;

namespace Limupa.Catalog.Api.Dtos.ProductDtos
{
    public class ResultProductİphonePhoneDto
    {
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductImageUrl { get; set; }
        public string ProductDescription { get; set; }
        public string CategoryID { get; set; }
        public string? SavedUrl { get; set; }
        public string? SavedFileName { get; set; }
        public string ProductUrlSeo { get; set; }
        public ResultCategoryDto Category { get; set; }
    }
}
