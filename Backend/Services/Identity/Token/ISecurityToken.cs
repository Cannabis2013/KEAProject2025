using ALBackend.Entities.Identity;

namespace ALBackend.Services.Identity.Token;

public interface ISecurityToken
{
    Task<string> Create(UserAccount user);
}