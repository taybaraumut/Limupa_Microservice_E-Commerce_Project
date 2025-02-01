using Limupa.DtoLayer.AccountDtos;

namespace Limupa.UI.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<bool> SignIn(AccountLoginDto accountLoginDto);
        Task<bool> GetRefreshToken();
    }
}
