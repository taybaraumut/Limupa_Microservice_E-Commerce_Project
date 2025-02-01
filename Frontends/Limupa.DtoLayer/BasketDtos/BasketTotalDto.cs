using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Limupa.DtoLayer.BasketDtos
{
    public class BasketTotalDto
    {
        public string UserID { get; set; }
        public string DiscountCode { get; set; }
        public int? DiscountRate { get; set; }
        public List<BasketItemDto> BasketItems { get; set; }
        public decimal TotalPrice
        {
            get
            {
                if (BasketItems == null)
                    return 0; // veya başka bir varsayılan değer döndürebilirsiniz

                return BasketItems.Sum(x => x.Price * x.Quantity);
            }
        }
    }
}
