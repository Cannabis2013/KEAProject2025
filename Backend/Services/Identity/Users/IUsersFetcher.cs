using System.Security.Claims;
using ALBackend.DataTransferObject.Identity;
using ALBackend.Entities.Identity;

namespace ALBackend.Services.Identity.Users;

public interface IUsersFetcher
{
    UserAccount? User(LoginRequest request);
    UserAccount? User(JwtCredentials credentials);
    UserAccount? User(Guid id);
    Task<UserAccountWithRoles?> UserWithRoles(Guid id);
    UserAccount? User(ClaimsPrincipal? principal);
    List<UserAccount> Users();
    List<UserAccount> ListUsers();
    
}