
using Limupa.UI.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Net;
using System.Net.Http.Headers;

namespace Limupa.UI.Handlers
{
    public class ResourceOwnerPasswordTokenHandler:DelegatingHandler
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IIdentityService ıdentityService;
        public ResourceOwnerPasswordTokenHandler(IHttpContextAccessor httpContextAccessor, IIdentityService ıdentityService)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.ıdentityService = ıdentityService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var accessToken = await httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer",accessToken);

            var response = await base.SendAsync(request,cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var tokenResponse = await ıdentityService.GetRefreshToken();
                if (tokenResponse != null)
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    response = await base.SendAsync(request, cancellationToken);
                }
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("Error");
            }

            return response;
        }
    }
}
