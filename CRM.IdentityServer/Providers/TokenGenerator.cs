using CRM.Core.Common;
using CRM.IdentityServer.Entities;
using CRM.IdentityServer.Model;
using CRM.IdentityServer.Repositories;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CRM.IdentityServer.Providers {
    public class TokenGenerator {

        public static TokenProviderOptions GetTokenProviderOptions() {
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.Config.GetSection("TokenAuthentication:client_secret").Value));

            return new TokenProviderOptions {
                Path = Configuration.Config.GetSection("TokenAuthentication:TokenPath").Value,
                Audience = Configuration.Config.GetSection("TokenAuthentication:Audience").Value,
                Issuer = Configuration.Config.GetSection("TokenAuthentication:Issuer").Value,
                Expiration = TimeSpan.FromMinutes(Convert.ToInt32(Configuration.Config.GetSection("TokenAuthentication:ExpirationMinutes").Value)),
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            };
        }

        public static LoginResponseData GenerateToken(ApplicationUser user, IRefreshTokenRepository repository, string deviceId, string client_id){
            var nowDateTime = DateTime.UtcNow;
            var options = GetTokenProviderOptions();

            //build and insert refresh token
            repository.RemoveRefreshToken(user.Id, client_id);

            var refreshToken = new RefreshToken();
            refreshToken.UserId = user.Id;
            refreshToken.ProtectedTicket = Guid.NewGuid().ToString("n");
            refreshToken.ClientId = client_id;
            refreshToken.DeviceId = deviceId;
            refreshToken.UserName = user.UserName;
            refreshToken.IssuedUtc = nowDateTime;
            refreshToken.ExpiresUtc = nowDateTime.Add(options.Expiration);

            repository.AddRefreshToken(refreshToken);

            //build user claims
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(nowDateTime).ToUniversalTime().ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
            };
            foreach (var userClaim in user.Claims)
                claims.Add(new Claim(userClaim.Type, userClaim.Value));

            //build bearer auth token
            var jwt = new JwtSecurityToken(
                issuer: options.Issuer,
                audience: options.Audience,
                claims: claims.ToArray(),
                notBefore: nowDateTime,
                expires: nowDateTime.Add(options.Expiration),
                signingCredentials: options.SigningCredentials);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            //build and return LoginResponseData
            var response = new LoginResponseData {
                Access_token = encodedJwt,
                Refresh_token = refreshToken.ProtectedTicket,
                Expires_in = (int)options.Expiration.TotalSeconds,
                UserName = user.UserName,
                Ident = user.Ident,
                Code = user.Code,
                Origin = user.Origin,
                Client_id = client_id,
                Token_type = "Bearer",
                Issued = nowDateTime,
                Expires = nowDateTime.Add(options.Expiration)
            };
            return response;
        }

    }
}
