namespace Limupa.Discount.Api.Entities
{
    public class Coupon
    {
        public int CouponID { get; set; }
        public string CouponCode { get; set; }
        public int CouponRate { get; set; }
        public bool CouponIsActive { get; set; }
        public DateTime CouponValidDate { get; set; }
    }
}
