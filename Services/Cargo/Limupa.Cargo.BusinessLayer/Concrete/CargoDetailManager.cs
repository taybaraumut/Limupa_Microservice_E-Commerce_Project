using Limupa.Cargo.BusinessLayer.Abstract;
using Limupa.Cargo.DataAccessLayer.Abstract;
using Limupa.Cargo.EntityLayer.Concrete;

namespace Limupa.Cargo.BusinessLayer.Concrete
{
    public class CargoDetailManager:ICargoDetailService
    {
        private readonly ICargoDetailDal cargoDetailDal;

        public CargoDetailManager(ICargoDetailDal cargoDetailDal)
        {
            this.cargoDetailDal = cargoDetailDal;
        }

        public async Task CreateAsync(CargoDetail t)
        {
            await cargoDetailDal.CreateAsync(t);
        }

        public async Task DeleteAsync(int id)
        {
            await cargoDetailDal.DeleteAsync(id);
        }

        public async Task<List<CargoDetail>> GetAllAsync()
        {
            var values = await cargoDetailDal.GetAllAsync();
            return values;
        }

        public async Task<CargoDetail> GetByIdAsync(int id)
        {
            var value = await cargoDetailDal.GetByIdAsync(id);
            return value;
        }

        public async Task UpdateAsync(CargoDetail t)
        {
            await cargoDetailDal.UpdateAsync(t);
        }
    }
}
