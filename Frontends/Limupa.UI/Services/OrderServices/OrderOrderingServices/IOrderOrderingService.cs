using Limupa.DtoLayer.OrderDtos.OrderOrderingDtos;

namespace Limupa.UI.Services.OrderServices.OrderOrderingServices
{
    public interface IOrderOrderingService
    {
        Task<List<ResultOrderingByUserIdDto>> GetOrderingByUserId(string userId);
    }
}
