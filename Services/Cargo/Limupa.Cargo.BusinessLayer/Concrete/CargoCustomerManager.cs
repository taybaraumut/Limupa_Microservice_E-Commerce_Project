using Limupa.Cargo.BusinessLayer.Abstract;
using Limupa.Cargo.DataAccessLayer.Abstract;
using Limupa.Cargo.EntityLayer.Concrete;

namespace Limupa.Cargo.BusinessLayer.Concrete
{
    public class CargoCustomerManager:ICargoCustomerService
    {
        private readonly ICargoCustomerDal cargoCustomerDal;

        public CargoCustomerManager(ICargoCustomerDal cargoCustomerDal)
        {
            this.cargoCustomerDal = cargoCustomerDal;
        }

        public async Task CreateAsync(CargoCustomer t)
        {
            await cargoCustomerDal.CreateAsync(t);
        }

        public async Task DeleteAsync(int id)
        {
            await cargoCustomerDal.DeleteAsync(id);
        }

        public async Task<List<CargoCustomer>> GetAllAsync()
        {
            var values = await cargoCustomerDal.GetAllAsync();
            return values;
        }

        public async Task<CargoCustomer> GetByIdAsync(int id)
        {
            var value = await cargoCustomerDal.GetByIdAsync(id);
            return value;
        }

        public async Task UpdateAsync(CargoCustomer t)
        {
            await cargoCustomerDal.UpdateAsync(t);
        }
    }
}
