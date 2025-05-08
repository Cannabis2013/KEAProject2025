namespace ALAdmin.Services.Auth.Storage;

public interface IAuthStorage
{
    public Task<string> AccessToken();
    public Task<string> RefreshToken();
    public Task SaveAccessToken(string accessToken);
    public Task SaveRefreshToken(string refreshToken);
    public Task<string> UserMail();
    public Task SaveUserMail(string email);
    public Task<string> UserId();
    public Task SaveUserId(string userId);
    public Task Clear();
}