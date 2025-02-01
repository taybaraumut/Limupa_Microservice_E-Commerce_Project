using System.Security.Claims;
using Limupa.UI.Services.Interfaces;

namespace Limupa.UI.Services.Concrete
{
    public class AccountLoginService : IAccountLoginService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public AccountLoginService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string GetUserID => httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
    }
}
