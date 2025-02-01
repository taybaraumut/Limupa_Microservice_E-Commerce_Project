using Limupa.Cargo.DataAccessLayer.Abstract;
using Limupa.Cargo.DataAccessLayer.Concrete.Context;
using Limupa.Cargo.DataAccessLayer.Repositories;
using Limupa.Cargo.EntityLayer.Concrete;

namespace Limupa.Cargo.DataAccessLayer.EntityFramework
{
    public class EfCargoOperationDal:GenericRepository<CargoOperation>,ICargoOperationDal
    {
        public EfCargoOperationDal(CargoContext cargoContext):base(cargoContext)
        {
            
        }
    }
}
