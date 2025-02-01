using IdentityModel;
using IdentityModel.Client;
using Limupa.UI.Settings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Limupa.IdentityServer.Tools
{
    public class JwtTokenGenerator
    {
        public static TokenResponseViewModel GeneratorToken(GetCheckAppUserViewModel getCheckAppUserViewModel)
        {
            ClientSettings clientSettings = new ClientSettings();

            var claims = new List<Claim>();

            // Kontrol Noktası Kullanıcının Role Türünün Boş Ve Boşluk Olmadığının Kontrolü Yapılıyor Tabikide String Format için                   
            if (!string.IsNullOrWhiteSpace(getCheckAppUserViewModel.Role))

                // Eğer Kontrol İf Bloğundan Geçer İse Claimse Ekleme Yapar Role Türünü Ve ID Ekler
                claims.Add(new Claim(ClaimTypes.Role, getCheckAppUserViewModel.Role));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, getCheckAppUserViewModel.ID));

            // Kontrol Noktası Kullanıcının Username Türünün Boş Ve Boşluk Olmadığının Kontrolü Yapılıyor Tabikide String Format için
            if (!string.IsNullOrWhiteSpace(getCheckAppUserViewModel.Username))
                claims.Add(new Claim("Username", getCheckAppUserViewModel.Username));

            var userClaims = claims.ToList();

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

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefaults.Key));

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expireDate = DateTime.UtcNow.AddDays(JwtTokenDefaults.ExpireDate);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken
            (
                audience: JwtTokenDefaults.ValidAudience,
                issuer: JwtTokenDefaults.ValidIssuer,
                claims:claims,
                signingCredentials: signingCredentials,
                expires: expireDate,
                notBefore: DateTime.UtcNow
            );

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            return new TokenResponseViewModel(jwtSecurityTokenHandler.WriteToken(jwtSecurityToken), expireDate);
        }
    }
}
