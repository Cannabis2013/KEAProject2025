using System.Security.Claims;
using ALBackend.DataTransferObject.Identity;
using ALBackend.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace ALBackend.Services.Identity.Users;

public class UsersFetcher(UserManager<UserAccount> userManager) : IUsersFetcher
{
    public UserAccount? User(LoginCredentials credentials)
    {
        var user = userManager
            .Users
            .FirstOrDefault(u => u.Email == credentials.Email);
        return user;
    }

    public UserAccount? User(JwtCredentials credentials)
    {
        return userManager
            .Users
            .FirstOrDefault(u => u.Email == credentials.Email && 
                                 u.RefreshToken == credentials.RefreshToken);
    }

    public UserFetchResponse? User(Guid id)
    {
        var user = userManager.Users.FirstOrDefault(u => u.Id == id.ToString());
        if (user is null) return null;
        return new()
        {
            Id = Guid.Parse(user.Id),
            Email = user.Email ?? "",
            Username = user.UserName ?? ""
        };
    }

    public async Task<UserFetchResponse?> UserWithRoles(Guid id)
    {
        var user = userManager.Users.FirstOrDefault(u => u.Id == id.ToString());
        if (user is null) return null;
        var roles = await userManager.GetRolesAsync(user!);
        return new()
        {
            Id = Guid.Parse(user.Id),
            Email = user.Email ?? "",
            Username = user.UserName ?? "",
            Roles = roles.ToList()
        };
    }

    public UserAccount? User(ClaimsPrincipal? principal)
    {
        if (principal is null) return null;
        var userId = principal.FindFirst(claim =>  claim.Type == "Id" )?.Value;
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