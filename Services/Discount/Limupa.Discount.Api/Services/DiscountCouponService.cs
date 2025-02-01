using Dapper;
using Limupa.Discount.Api.Dtos.DiscountCouponDtos;
using Limupa.Discount.Context;
using Limupa.Discount.Dtos.DiscountCouponDtos;

namespace Limupa.Discount.Services
{
    public class DiscountCouponService : IDiscountCouponService
    {
        private readonly DiscountContext discountContext;

        public DiscountCouponService(DiscountContext discountContext)
        {
            this.discountContext = discountContext;
        }

        public async Task CreateDiscountCouponAsync(CreateDiscountCouponDto createDiscountCouponDto)
        {
            string query = "Insert Into Coupons (CouponCode,CouponRate,CouponIsActive,CouponValidDate) values (@couponCode,@couponRate,@couponIsActive,@couponValidDate)";
            var parameters = new DynamicParameters();
            parameters.Add("@couponCode",createDiscountCouponDto.CouponCode);
            parameters.Add("@couponRate",createDiscountCouponDto.CouponRate);
            parameters.Add("@couponIsActive",createDiscountCouponDto.CouponIsActive);
            parameters.Add("@couponValidDate",createDiscountCouponDto.CouponValidDate);

            using (var connect = discountContext.CreateConnection())
            {
                await connect.ExecuteAsync(query,parameters);
            }
        }

        public async Task DeleteDiscountCouponAsync(int id)
        {
            string query = "Delete From Coupons Where CouponID=@couponID";
            var parameters = new DynamicParameters();
            parameters.Add("@couponID",id);

            using (var connect = discountContext.CreateConnection())
            {
                await connect.ExecuteAsync(query,parameters);
            }
        }

        public async Task<List<ResultDiscountCouponDto>> GetAllDiscountCouponAsync()
        {
            string query = "Select * From Coupons";
            using (var connect = discountContext.CreateConnection())
            {
                var values = await connect.QueryAsync<ResultDiscountCouponDto>(query);
                return values.ToList();
            }
        }

        public async Task<GetByIdDiscountCouponDto> GetByIdDiscountCouponAsync(int id)
        {
            string query = "Select * From Coupons Where CouponID=@couponID";
            var parameters = new DynamicParameters();
            parameters.Add("@couponID",id);
            using (var connect = discountContext.CreateConnection())
            {
                var values = await connect.QueryFirstOrDefaultAsync<GetByIdDiscountCouponDto>(query,parameters);
                return values!;
            }
        }

        public async Task<GetDiscountCodeDetailByCode> GetCodeDetailByCodeAsync(string code)
        {
            string query = "Select * From Coupons Where CouponCode=@code";
            var parameters = new DynamicParameters();
            parameters.Add("@code",code);
            using (var connect = discountContext.CreateConnection())
            {
                var values = await connect.QueryFirstOrDefaultAsync<GetDiscountCodeDetailByCode>(query, parameters);
                return values!;
            }
        }

        public int GetDiscountCouponCountRateAsync(string code)
        {
            string query = "Select CouponRate From Coupons Where CouponCode=@code";
            var parameters = new DynamicParameters();
            parameters.Add("@code", code);
            using (var connect = discountContext.CreateConnection())
            {
                var values = connect.QueryFirstOrDefault<int>(query, parameters);
                return values;
            }
        }

        public async Task UpdateDiscountCouponAsync(UpdateDiscountCouponDto updateDiscountCouponDto)
        {
            string query = "Update Coupons Set CouponCode=@couponCode,CouponRate=@couponRate,CouponIsActive=@couponIsActive,CouponValidDate=@couponValidDate where CouponID=@couponID";
            var parameters = new DynamicParameters();
            parameters.Add("@couponCode", updateDiscountCouponDto.CouponCode);
            parameters.Add("@couponRate", updateDiscountCouponDto.CouponRate);
            parameters.Add("@couponIsActive", updateDiscountCouponDto.CouponIsActive);
            parameters.Add("@couponValidDate", updateDiscountCouponDto.CouponValidDate);
            parameters.Add("@couponID", updateDiscountCouponDto.CouponID);

            using (var connect = discountContext.CreateConnection())
            {
                await connect.ExecuteAsync(query, parameters);
            }
        }
    }
}
