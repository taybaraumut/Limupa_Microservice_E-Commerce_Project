using Limupa.DtoLayer.CargoDtos.CargoCompanyDtos;

namespace Limupa.UI.Services.CargoServices.CargoCompanyServices
{
    public interface ICargoCompanyService
    {
        Task<List<ResultCargoCompanyDto>> GetAllCargoCompanyAsync();
        Task CreateCargoCompanyAsync(CreateCargoCompanyDto createCargoCompanyDto);
        Task UpdateCargoCompanyAsync(UpdateCargoCompanyDto updateCargoCompanyDto);
        Task<GetByIdCargoCompanyDto> GetByIdCargoCompanyAsync(string id);
        Task DeleteCargoCompanyAsync(string id);
    }
}
