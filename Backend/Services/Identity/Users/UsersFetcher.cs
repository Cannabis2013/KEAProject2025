using System.Security.Claims;
using ALBackend.DataTransferObject.Identity;
using ALBackend.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace ALBackend.Services.Identity.Users;

public class UsersFetcher(UserManager<UserAccount> userManager) : IUsersFetcher
{
    public UserAccount? User(LoginRequest request)
    {
        var user = userManager
            .Users
            .FirstOrDefault(u => u.Email == request.Email);
        return user;
    }

    public UserAccount? User(JwtCredentials credentials)
    {
        return userManager
            .Users
            .FirstOrDefault(u => u.Email == credentials.Email &&
                                 u.RefreshToken == credentials.RefreshToken);
    }

    public UserAccount? User(Guid id)
    {
        var user = userManager.Users.FirstOrDefault(u => u.Id == id.ToString());
        return user ?? null;
    }

    public async Task<UserAccountWithRoles?> UserWithRoles(Guid id)
    {
        var user = userManager.Users.FirstOrDefault(u => u.Id == id.ToString());
        if (user is null) return null;
        var roles = await userManager.GetRolesAsync(user!);
        return new()
        {
            User = user,
            Roles = roles.ToList()
        };
    }

    public UserAccount? User(ClaimsPrincipal? principal)
    {
        var authenticated = principal?.Identity?.IsAuthenticated ?? false;
        if (principal is null || !authenticated) return null;
        var userId = principal.FindFirst(claim => claim.Type == "Id")?.Value;
        return userManager.Users.First(u => u.Id == userId);
    }

    public List<UserAccount> Users()
    {
        return userManager
            .Users
            .OrderBy(user => user.UserName)
            .ToList();
    }

    public List<UserAccount> ListUsers()
    {
        return userManager
            .Users
            .OrderBy(user => user.UserName)
            .ToList();
    }
}