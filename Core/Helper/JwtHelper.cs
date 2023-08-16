using Entities.JWTEntity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Core.Helper;

public class JwtHelper
{
    public static TokenModel GetJwtToken(JwtSettings jwtSettings, List<Claim> claims)
    {
        TokenModel TokenModel = new();

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SigningKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        TokenModel.Expiration = DateTime.UtcNow.AddMinutes(60);

        JwtSecurityToken tokens = new
            (
              issuer: jwtSettings.Issuer,
              audience: jwtSettings.Audience,
              notBefore: DateTime.UtcNow,
              expires: TokenModel.Expiration,
              claims: claims,
              signingCredentials: creds
            );
        TokenModel.Token = new JwtSecurityTokenHandler().WriteToken(tokens);
        return TokenModel;
    }
}