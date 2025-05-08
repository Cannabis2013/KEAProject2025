using ALBackend.Entities.Identity;

namespace ALBackend.Services.Identity.Users;

public interface IUsersUpdate
{
    Task<Guid> CreateAsync(UserCredentials credentials);
    Task RemoveAsync(Guid id);
    Task UpdateAsync(UserCredentials request);
}