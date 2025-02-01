using Limupa.UI.Models;
using Limupa.UI.Services.Interfaces;

namespace Limupa.UI.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly HttpClient httpClient;

        public UserService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<UserDetailViewModel> GetUserInfo()
        {
            return await httpClient.GetFromJsonAsync<UserDetailViewModel>("http://localhost:5001/api/profiles/getuserx");
        }
    }
}
