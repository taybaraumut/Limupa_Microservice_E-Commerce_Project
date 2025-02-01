using Limupa.UI.Models;

namespace Limupa.UI.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDetailViewModel> GetUserInfo();
    }
}
