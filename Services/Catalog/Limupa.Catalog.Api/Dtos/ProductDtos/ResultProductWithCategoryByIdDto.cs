using Limupa.Catalog.Api.Dtos.ProductImageDtos;
using Limupa.Catalog.Dtos.CategoryDtos;

namespace Limupa.Catalog.Api.Dtos.ProductDtos
{
    public class ResultProductWithCategoryByIdDto
    {
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductImageUrl { get; set; }
        public string ProductDescription { get; set; }
        public string CategoryID { get; set; }
        public ResultCategoryDto Category { get; set; }
        public List<ResultProductImageWithProductDto> Images { get; set; }
    }
}
