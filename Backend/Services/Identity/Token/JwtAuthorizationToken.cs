using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ALBackend.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace ALBackend.Services.Identity.Token;

public class JwtAuthorizationToken(IConfiguration appConfig, UserManager<UserAccount> userManager)
    : ISecurityToken
{
    public async Task<string> Create(UserAccount user)
    {
        var roles = await userManager.GetRolesAsync(user);
        var identityClaim = Claims(user, roles);
        var tokenDescriptor = TokenDescriptor(identityClaim);
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private SecurityTokenDescriptor TokenDescriptor(ClaimsIdentity identity)
    {
        const string fallBack = "ABCDEFGHIJKLMNOPQRSTUVXY";
        var key = Encoding.ASCII.GetBytes(appConfig["Jwt:Key"] ?? fallBack);
        var symmetricKey = new SymmetricSecurityKey(key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = identity,
            Expires = DateTime.UtcNow.AddMinutes(60),
            Issuer = appConfig["Jwt:Issuer"],
            Audience = appConfig["Jwt:Audience"],
            SigningCredentials = new(symmetricKey,
                SecurityAlgorithms.HmacSha512Signature)
        };
        return tokenDescriptor;
    }

    // ReSharper disable once MemberCanBeMadeStatic.Local
    private static ClaimsIdentity Claims(UserAccount user, IEnumerable<string> roles)
    {
        var claims = new List<Claim>()
        {
            new("Id", user.Id),
            new("Username", user.UserName!),
            new(JwtRegisteredClaimNames.Email, user.UserName!),
            new(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
        };
        claims.AddRange(roles
            .Select(r => new Claim(ClaimTypes.Role, r)));
        return new(claims);
    }
}