using Limupa.Order.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Limupa.Order.Persistence.Context
{
    public class OrderContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1434;initial Catalog=LimupaOrderDb;Trusted_Connection=false;TrustServerCertificate=true;User=sa;Password=Admin3462");
        }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Ordering> Orderings { get; set; }
    }
}
