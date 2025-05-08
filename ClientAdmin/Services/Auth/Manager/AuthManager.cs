using System.Net;
using ALAdmin.DataTransferObjects.Auth;
using ALAdmin.Services.Auth.Request;
using ALAdmin.Services.Auth.Storage;

namespace ALAdmin.Services.Auth.Manager;

public class AuthManager(HttpClient client, IAuthRequest request,IAuthStorage storage) : IAuthManager
{
    private const string EndpointUri = "/auth/admin";

    private async Task UpdateAuthStorage(AuthLoginResponse response)
    {
        await storage.SaveAccessToken(response.AccessToken);
        await storage.SaveRefreshToken(response.RefreshToken);
        await storage.SaveUserId(response.Id.ToString());
        await storage.SaveUserMail(response.Email);
    }

    public async Task<bool> LoginAsync(AuthLoginRequest credentials)
    {
        AuthLoginResponse? loginResponse;
        try
        {
            var response = await client.PostAsJsonAsync($"{EndpointUri}/login",credentials);
            if (response.StatusCode == HttpStatusCode.Forbidden)
                return false;
            loginResponse = await response.Content.ReadFromJsonAsync<AuthLoginResponse>();
        }
        catch (Exception)
        {
            return false;
        }
        if (loginResponse is null)
            return false;
        await UpdateAuthStorage(loginResponse);
        return true;
    }

    public async Task LogoutAsync() => await storage.Clear();

    public async Task<bool> IsSignedInAsync()
    {
        try
        {
            return await request.GetAsync<bool>($"{EndpointUri}/check");
        }
        catch (Exception)
        {
            return false;
        }
    }
}