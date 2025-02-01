using Limupa.Cargo.BusinessLayer.Abstract;
using Limupa.Cargo.DataAccessLayer.Abstract;
using Limupa.Cargo.EntityLayer.Concrete;

namespace Limupa.Cargo.BusinessLayer.Concrete
{
    public class CargoCompanyManager : ICargoCompanyService
    {
        private readonly ICargoCompanyDal cargoCompanyDal;

        public CargoCompanyManager(ICargoCompanyDal cargoCompanyDal)
        {
            this.cargoCompanyDal = cargoCompanyDal;
        }

        public async Task CreateAsync(CargoCompany t)
        {
           await cargoCompanyDal.CreateAsync(t);
        }

        public async Task DeleteAsync(int id)
        {
            await cargoCompanyDal.DeleteAsync(id);
        }

        public async Task<List<CargoCompany>> GetAllAsync()
        {
            var values = await cargoCompanyDal.GetAllAsync();
            return values;
        }

        public async Task<CargoCompany> GetByIdAsync(int id)
        {
            var value = await cargoCompanyDal.GetByIdAsync(id);
            return value;
        }

        public async Task UpdateAsync(CargoCompany t)
        {
            await cargoCompanyDal.UpdateAsync(t);
        }
    }
}
