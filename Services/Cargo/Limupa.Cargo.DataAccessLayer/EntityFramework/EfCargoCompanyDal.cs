using Limupa.Cargo.DataAccessLayer.Abstract;
using Limupa.Cargo.DataAccessLayer.Concrete.Context;
using Limupa.Cargo.DataAccessLayer.Repositories;
using Limupa.Cargo.EntityLayer.Concrete;

namespace Limupa.Cargo.DataAccessLayer.EntityFramework
{
    public class EfCargoCompanyDal : GenericRepository<CargoCompany>, ICargoCompanyDal
    {
        public EfCargoCompanyDal(CargoContext cargoContext):base(cargoContext)
        {
                
        }
    }
}
