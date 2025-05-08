using ALAdmin.DataTransferObjects.Auth;

namespace ALAdmin.Services.Auth.Manager;

public interface IAuthManager
{
    public Task<bool> LoginAsync(AuthLoginRequest request);
    public Task LogoutAsync();
    public Task<bool> IsSignedInAsync();
}