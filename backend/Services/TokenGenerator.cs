using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.Constants;
using backend.Models;
using Microsoft.IdentityModel.Tokens;

namespace backend.Services;

public class TokenGenerator
{
    public string Generate(Account account)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        
        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Subject = AddClaims(account),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(TokenConstant.Secret)),
                SecurityAlgorithms.HmacSha256Signature)
        });
        
        return tokenHandler.WriteToken(token);
    }
    
    private ClaimsIdentity AddClaims(Account account) 

    {

        var claims = new ClaimsIdentity();

        claims.AddClaim(new Claim(ClaimTypes.Role, account.Role.ToString()));
        
        return claims;

    }
}