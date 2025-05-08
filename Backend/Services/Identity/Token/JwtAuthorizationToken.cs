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
    public async Task<string> Token(UserAccount user)
    {
        var roles = await userManager.GetRolesAsync(user);
        var tokenDescriptor = TokenDescriptor(user,roles);
        return Token(tokenDescriptor);
    }

    private SecurityTokenDescriptor TokenDescriptor(UserAccount user, IEnumerable<string> roles)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = Identity(user,roles),
            Expires = DateTime.UtcNow.AddMinutes(60),
            Issuer = appConfig["Jwt:Issuer"],
            Audience = appConfig["Jwt:Audience"],
            SigningCredentials = new(new SymmetricSecurityKey(Key()),
                SecurityAlgorithms.HmacSha512Signature)
        };
        return tokenDescriptor;
    }

    // ReSharper disable once MemberCanBeMadeStatic.Local
    private static ClaimsIdentity Identity(UserAccount user,IEnumerable<string> roles)
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

    private byte[] Key()
    {
        const string fallBack = "ABCDEFGHIJKLMNOPQRSTUVXY";
        return Encoding.ASCII.GetBytes(appConfig["Jwt:Key"] ?? fallBack);
    }

    private static string Token(SecurityTokenDescriptor descriptor)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(descriptor);
        return tokenHandler.WriteToken(token);
    }
}