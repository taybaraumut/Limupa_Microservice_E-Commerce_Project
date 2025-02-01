using Limupa.Cargo.DataAccessLayer.Abstract;
using Limupa.Cargo.DataAccessLayer.Concrete.Context;
using Limupa.Cargo.DataAccessLayer.Repositories;
using Limupa.Cargo.EntityLayer.Concrete;

namespace Limupa.Cargo.DataAccessLayer.EntityFramework
{
    public class EfCargoCustomerDal:GenericRepository<CargoCustomer>,ICargoCustomerDal
    {
        public EfCargoCustomerDal(CargoContext cargoContext):base(cargoContext)
        {
            
        }
    }
}
