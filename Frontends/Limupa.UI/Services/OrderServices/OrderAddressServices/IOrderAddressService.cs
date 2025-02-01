using Limupa.DtoLayer.OrderDtos.OrderAddressDtos;

namespace Limupa.UI.Services.OrderServices.OrderAddressServices
{
    public interface IOrderAddressService
    {
        //Task<List<ResultAboutDto>> GetAllAboutAsync();
        Task CreateOrderAddressAsync(CreateOrderAddressDto createOrderAddressDto);
        //Task UpdateAboutAsync(UpdateAboutDto updateAboutDto);
        //Task<GetByIdAboutDto> GetByIdAboutAsync(string id);
        //Task DeleteAboutAsync(string id);
    }
}
