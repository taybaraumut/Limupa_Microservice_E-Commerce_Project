namespace Limupa.Discount.Dtos.DiscountCouponDtos
{
    public class GetByIdDiscountCouponDto
    {
        public int CouponID { get; set; }
        public string CouponCode { get; set; }
        public int CouponRate { get; set; }
        public bool CouponIsActive { get; set; }
        public DateTime CouponValidDate { get; set; }
    }
}
