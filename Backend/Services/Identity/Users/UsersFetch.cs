using System.Security.Claims;
using ALBackend.DataTransferObject.Identity;
using ALBackend.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace ALBackend.Services.Identity.Users;

public class UsersFetch(UserManager<UserAccount> userManager) : IUsersFetch
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

    public UserAccount? User(Guid id)
    {
        return userManager.Users.First(u => u.Id == id.ToString());
    }

    public UserAccount? User(ClaimsPrincipal? principal)
    {
        if (principal is null) return null;
        var userId = principal.FindFirst(claim =>  claim.Type == "Id" )?.Value;
        return userId == null ? null : User(Guid.Parse(userId));
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