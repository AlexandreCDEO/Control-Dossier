using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Control_Dossier.Models;
using Microsoft.IdentityModel.Tokens;

namespace Control_Dossier.Service;

public class TokenService
{
    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
        var tokenDercriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddHours(8),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDercriptor);
        return tokenHandler.WriteToken(token);
    }
}