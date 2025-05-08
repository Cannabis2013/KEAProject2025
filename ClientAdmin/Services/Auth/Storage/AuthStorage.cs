using Blazored.LocalStorage;

namespace ALAdmin.Services.Auth.Storage;

public class AuthStorage(ILocalStorageService localStorage) : IAuthStorage
{
    public async Task<string> AccessToken()
    {
        return await localStorage.GetItemAsStringAsync("JwtAccess") ?? "";
    }

    public async Task<string> RefreshToken()
    {
        return await localStorage.GetItemAsStringAsync("JwtRefresh") ?? "";
    }

    public async Task SaveAccessToken(string accessToken)
    {
        await localStorage.SetItemAsStringAsync("JwtAccess", accessToken);
    }

    public async Task SaveRefreshToken(string refreshToken)
    {
        await localStorage.SetItemAsStringAsync("JwtRefresh", refreshToken);
    }

    public async Task<string> UserMail()
    {
        return await localStorage.GetItemAsStringAsync("UserMail") ?? "";
    }

    public async Task SaveUserMail(string email)
    {
        await localStorage.SetItemAsStringAsync("UserMail", email);
    }

    public async Task<string> UserId()
    {
        return await localStorage.GetItemAsStringAsync("UserId") ?? "";
    }

    public async Task SaveUserId(string userId)
    {
        await localStorage.SetItemAsStringAsync("UserId", userId);
    }

    public async Task Clear()
    {
        await localStorage.ClearAsync();
    }
}