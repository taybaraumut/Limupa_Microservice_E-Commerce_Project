using IdentityModel;
using IdentityModel.Client;
using Limupa.DtoLayer.AccountDtos;
using Limupa.UI.Services.Interfaces;
using Limupa.UI.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Data;
using System.Security.Claims;

namespace Limupa.UI.Services.Concrete
{
    public class IdentityService : IIdentityService
    {
        private readonly HttpClient httpClient;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ClientSettings clientSettings;
        private readonly ServiceApiSettings serviceApiSettings;

        public IdentityService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IOptions<ClientSettings> clientSettings, IOptions<ServiceApiSettings> serviceApiSettings)
        {
            this.httpClient = httpClient;
            this.httpContextAccessor = httpContextAccessor;
            this.clientSettings = clientSettings.Value;
            this.serviceApiSettings = serviceApiSettings.Value;
        }

        public async Task<bool> GetRefreshToken()
        {
            var discoveryEndPoint = await httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = serviceApiSettings.IdentityUrl,
                Policy = new DiscoveryPolicy
                {
                    RequireHttps = false
                }
            });

            var refreshToken = await httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);


            RefreshTokenRequest refreshTokenRequest = new()
            {
                ClientId = clientSettings.LimupaAdminClient.ClientId,
                ClientSecret = clientSettings.LimupaAdminClient.ClientSecret,
                RefreshToken = refreshToken,
                Address = discoveryEndPoint.TokenEndpoint
            };

            var token = await httpClient.RequestRefreshTokenAsync(refreshTokenRequest);

            var authenticationToken = new List<AuthenticationToken>()
            {
                new AuthenticationToken
                {
                    Name=OpenIdConnectParameterNames.AccessToken,
                    Value = token.AccessToken
                },
                new AuthenticationToken
                {
                    Name=OpenIdConnectParameterNames.RefreshToken,
                    Value = token.RefreshToken
                },
                new AuthenticationToken
                {
                    Name=OpenIdConnectParameterNames.ExpiresIn,
                    Value=DateTime.Now.AddSeconds(token.ExpiresIn).ToString()
                }
            };

            var result = await httpContextAccessor.HttpContext.AuthenticateAsync();

            var properties = result.Properties;
            properties.StoreTokens(authenticationToken);

            await httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, result.Principal, properties);

            return true;
        }

        public async Task<bool> SignIn(AccountLoginDto accountLoginDto)
        {
            var discoveryEndPoint = await httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = "http://localhost:5001",
                Policy = new DiscoveryPolicy
                {
                    RequireHttps = false,
                }
            });

            var passwordTokenRequest = new PasswordTokenRequest
            {
                ClientId = clientSettings.LimupaAdminClient.ClientId,
                ClientSecret = clientSettings.LimupaAdminClient.ClientSecret,
                UserName = accountLoginDto.Username,
                Password = accountLoginDto.Password,
                Address = discoveryEndPoint.TokenEndpoint,
            };

            var tokenResponse = await httpClient.RequestPasswordTokenAsync(passwordTokenRequest);

            if (tokenResponse.IsError)
            {
                // Handle error
                return false;
            }

            var accessToken = tokenResponse.AccessToken;

            var userInfoRequest = new UserInfoRequest
            {
                Token = accessToken,
                Address = discoveryEndPoint.UserInfoEndpoint,
            };

            var userInfoResponse = await httpClient.GetUserInfoAsync(userInfoRequest);

            if (userInfoResponse.IsError)
            {
                // Handle error
                return false;
            }



            var userClaims = userInfoResponse.Claims.ToList();

            // Kullanıcının rollerini al
            var roles = userClaims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList();

            // ClaimsIdentity oluştur
            var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme, JwtClaimTypes.Name, JwtClaimTypes.Role);
            // Rollerin eklenmesi
            foreach (var role in roles)
            {
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            // Token bilgileri için AuthenticationProperties oluştur
            var authenticationProperties = new AuthenticationProperties();
            authenticationProperties.StoreTokens(new List<AuthenticationToken>
                {
                    new AuthenticationToken
                    {
                       Name = OpenIdConnectParameterNames.AccessToken,
                       Value = tokenResponse.AccessToken
                    },
                    new AuthenticationToken
                    {
                       Name = OpenIdConnectParameterNames.RefreshToken,
                       Value = tokenResponse.RefreshToken
                    },
                    new AuthenticationToken
                    {
                       Name = OpenIdConnectParameterNames.ExpiresIn,
                       Value = DateTime.Now.AddSeconds(tokenResponse.ExpiresIn).ToString()
                    }
                 });
            authenticationProperties.IsPersistent = false;

            // Oturum açma işlemi
            await httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authenticationProperties);

            return true;
        }
    }
}
