using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PatikaWeek14PracticeDataProtectionAndJwt.Jwt
{
    public static class JwtHelper
    {
        
            public static string GenerateJwtToken(JwtDto jwtInfo)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtInfo.SecretKey));

                var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim("Id",jwtInfo.Id.ToString()),
                    new Claim("Email",jwtInfo.Email),
                    new Claim("UserType",jwtInfo.UserType.ToString()),

                    new Claim(ClaimTypes.Role , jwtInfo.UserType.ToString())
                };


                var expireTime = DateTime.Now.AddMinutes(jwtInfo.ExpireMinutes);

                var tokenDescriptor = new JwtSecurityToken(jwtInfo.Issuer, jwtInfo.Audience, claims, null, expireTime, credentials);

                var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

                return token;
            }
        
    }
}
