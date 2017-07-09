using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ShoppingCart.Repository;

namespace ShoppingCart.Jwt
{
    public interface ITokenProvider
    {
        string GetPath();
        Task<TokenResponse> GetToken(string userId = null);
    }

    public class JwtTokenProvider : ITokenProvider
    {
        private TokenProviderOptions _options;

        public JwtTokenProvider(TokenProviderOptions options)
        {
            _options = options;
        }

        public string GetPath()
        {
            return _options.Path;
        }

        public async Task<TokenResponse> GetToken(string userId = null)
        {
            var now = DateTime.UtcNow;
            var expire = now.Add(_options.Expiration);


            var claims = new List<Claim>();

            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            /*claims.Add(new Claim(JwtRegisteredClaimNames.Iat, 
                                 DateTimeOffset.FromUnixTimeSeconds(now.ToString()).ToString(),
                                 ClaimValueTypes.Integer64));
            if (userId != null)
            {
                claims.Add(new Claim(JwtRegisteredClaimNames.Sub, userId));
            }*/
                
            // Create the JWT and write it to a string

            var jwt = new JwtSecurityToken(
                claims: claims,
                notBefore: now,
                issuer: _options.Issuer,
                signingCredentials: _options.SigningCredentials,
                expires: expire);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new TokenResponse()
            {
                DataResponse = new TokenResponse.Data()
                {
					AccessToken = encodedJwt,
                    ExpireTime = (int) _options.Expiration.TotalSeconds   
                }
            };

            // Serialize and return the response
            return response;
        }
    }
}
