using Limupa.Cargo.EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Limupa.Cargo.DataAccessLayer.Concrete.Context
{
    public class CargoContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1435;initial Catalog=LimupaCargoDb;Trusted_Connection=false;TrustServerCertificate=true;User=sa;Password=Admin3462");
        }

        public DbSet<CargoCompany> CargoCompanies { get; set; }
        public DbSet<CargoCustomer> CargoCustomers { get; set; }
        public DbSet<CargoDetail> CargoDetails { get; set; }
        public DbSet<CargoOperation> CargoOperations { get; set; }
    }
}
