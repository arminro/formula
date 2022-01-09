using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using F1Web.Helpers;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace F1Web.Security
{
    /// <summary>
    /// Provides functionality for JWT token generation.
    /// </summary>
    public class JWTProvider : ITokenService
    {
        IOptions<AppSettings> _appSettings;

        /// <summary>
        /// Constructor for the class.
        /// </summary>
        /// <param name="appSettings">The object storing the appsettings.</param>
        public JWTProvider(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        ///<inheritdoc/>
        public string GenerateToken(Guid userId)
        {
           // based on: https://jasonwatmore.com/post/2018/08/14/aspnet-core-21-jwt-authentication-tutorial-with-example-api
           var tokenHandler = new JwtSecurityTokenHandler();
           var key = Encoding.ASCII.GetBytes(_appSettings.Value.Secret);

           var idClaim = new Claim[]{

               new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
           };

           SecurityTokenDescriptor tokenDescriptor = CreateTokenDescriptor(userId, idClaim, key);
           var token = tokenHandler.CreateToken(tokenDescriptor);
           return tokenHandler.WriteToken(token);
        }

        private static SecurityTokenDescriptor CreateTokenDescriptor(object dataRepresented, Claim[] claims, byte[] key)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                IssuedAt = DateTime.UtcNow,
            };
            return tokenDescriptor;
        }
    }
}
