using IdentityModel.AspNetCore.AccessTokenManagement;
using IdentityModel.Client;
using Limupa.DtoLayer.AccountDtos;
using Limupa.UI.Services.Interfaces;
using Limupa.UI.Settings;
using Microsoft.Extensions.Options;

namespace Limupa.UI.Services.Concrete
{
    public class ClientCredentialTokenService : IClientCredentialTokenService
    {
        private readonly ServiceApiSettings serviceApiSettings;
        private readonly HttpClient httpClient;
        private readonly IClientAccessTokenCache clientAccessTokenCache;
        private readonly ClientSettings clientSettings;

        public ClientCredentialTokenService(IOptions<ServiceApiSettings> serviceApiSettings, HttpClient httpClient, IClientAccessTokenCache clientAccessTokenCache,IOptions<ClientSettings> clientSettings)
        {
            this.serviceApiSettings = serviceApiSettings.Value;
            this.httpClient = httpClient;
            this.clientAccessTokenCache = clientAccessTokenCache;
            this.clientSettings = clientSettings.Value;
        }

        public async Task<string> GetToken()
        {
            var currentToken = await clientAccessTokenCache.GetAsync("token");
            if(currentToken != null)
            {
                return currentToken.AccessToken;
            }
            var discoveryEndPoint = await httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = serviceApiSettings.IdentityUrl,
                Policy = new DiscoveryPolicy
                {
                    RequireHttps = false
                }
            });

            var clientCredentialsTokenRequest = new ClientCredentialsTokenRequest
            {
                ClientId = clientSettings.LimupaMemberClient.ClientId,
                ClientSecret = clientSettings.LimupaMemberClient.ClientSecret,
                Address = discoveryEndPoint.TokenEndpoint
            };

            var newToken = await httpClient.RequestClientCredentialsTokenAsync(clientCredentialsTokenRequest);
            await clientAccessTokenCache.SetAsync("token",newToken.RefreshToken,newToken.ExpiresIn);
            return newToken.AccessToken;
        }
    }
}
