using Limupa.Discount.Api.Entities;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Data;
using System.Data.SqlClient;

namespace Limupa.Discount.Context
{
    public class DiscountContext:DbContext
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        public DiscountContext(IConfiguration configuration,DbContextOptions<DiscountContext> options):base(options)
        {
            this.configuration = configuration;
            connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }
        public DbSet<Coupon> Coupons { get; set; }
        public IDbConnection CreateConnection() => new MySqlConnection(connectionString);
    }
}
