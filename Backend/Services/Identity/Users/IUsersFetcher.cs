using System.Security.Claims;
using ALBackend.DataTransferObject.Identity;
using ALBackend.Entities.Identity;

namespace ALBackend.Services.Identity.Users;

public interface IUsersFetcher
{
    UserAccount? User(LoginCredentials credentials);
    UserAccount? User(JwtCredentials credentials);
    UserFetchResponse? User(Guid id);
    Task<UserFetchResponse?> UserWithRoles(Guid id);
    UserAccount? User(ClaimsPrincipal? principal);
    List<UserAccount> Users();
    List<UserAccount> ListUsers();
    
}