using Limupa.Cargo.BusinessLayer.Abstract;
using Limupa.Cargo.DataAccessLayer.Abstract;
using Limupa.Cargo.EntityLayer.Concrete;

namespace Limupa.Cargo.BusinessLayer.Concrete
{
    public class CargoOperationManager:ICargoOperationService
    {
        private readonly ICargoOperationDal cargoOperationDal;

        public CargoOperationManager(ICargoOperationDal cargoOperationDal)
        {
            this.cargoOperationDal = cargoOperationDal;
        }

        public async Task CreateAsync(CargoOperation t)
        {
            await cargoOperationDal.CreateAsync(t);
        }

        public async Task DeleteAsync(int id)
        {
            await cargoOperationDal.DeleteAsync(id);
        }

        public async Task<List<CargoOperation>> GetAllAsync()
        {
            var values = await cargoOperationDal.GetAllAsync();
            return values;
        }

        public async Task<CargoOperation> GetByIdAsync(int id)
        {
            var value = await cargoOperationDal.GetByIdAsync(id);
            return value;
        }

        public async Task UpdateAsync(CargoOperation t)
        {
            await cargoOperationDal.UpdateAsync(t);
        }
    }
}
