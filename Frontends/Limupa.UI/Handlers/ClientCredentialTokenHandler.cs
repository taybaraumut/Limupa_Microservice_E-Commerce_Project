
using Limupa.UI.Services.Interfaces;
using System.Net;
using System.Net.Http.Headers;

namespace Limupa.UI.Handlers
{
    public class ClientCredentialTokenHandler : DelegatingHandler
    {
        private readonly IClientCredentialTokenService clientCredentialTokenService;
        public ClientCredentialTokenHandler(IClientCredentialTokenService clientCredentialTokenService)
        {
            this.clientCredentialTokenService = clientCredentialTokenService;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await clientCredentialTokenService.GetToken());
            var response = await base.SendAsync(request, cancellationToken);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("Error");
            }
            return response;
        }
    }
}
