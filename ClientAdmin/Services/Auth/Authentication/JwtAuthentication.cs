using ALAdmin.DataTransferObjects.Auth;
using ALAdmin.Services.Auth.Request;
using ALAdmin.Services.Auth.Storage;
using Microsoft.AspNetCore.Components;
using static System.Net.HttpStatusCode;

namespace ALAdmin.Services.Auth.Authentication;

public class JwtAuthentication(
    HttpClient client,
    IAuthRequest request,
    IAuthStorage storage,
    NavigationManager navigation)
    : IAuthentication
{
    private async Task<object> Credentials()
    {
        return new
        {
            email = await storage.UserMail(),
            refreshToken = await storage.RefreshToken()
        };
    }

    private async Task UpdateAuthStorage(AuthRefreshResponse? response)
    {
        await storage.SaveAccessToken(response!.AccessToken);
        await storage.SaveRefreshToken(response.RefreshToken);
    }

    private async Task<bool> RefreshTokenAsync()
    {
        var refreshRequest = await Credentials();
        var response = await client.PostAsJsonAsync($"auth/refresh", refreshRequest);
        if (response.StatusCode is Unauthorized or Forbidden)
        {
            await storage.Clear();
            return false;
        }
        await UpdateAuthStorage(await response.Content.ReadFromJsonAsync<AuthRefreshResponse>());
        return true;
    }

    private async Task<HttpResponseMessage> FetchResponse()
    {
        return await client.GetAsync($"/auth/admin/check");
    }

    private async Task<HttpResponseMessage?> AuthenticationCheck()
    {
        try
        {
            return await request.GetAsync("/auth/admin/check");
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task AuthenticateAsync()
    {
        var response = await AuthenticationCheck();
        if (response is null)
            navigation.NavigateTo("/NoConnection", true);
        else if (response.StatusCode == Forbidden)
            navigation.NavigateTo("/ForbiddenAccess", true);
        else if (response.StatusCode == Unauthorized && !await RefreshTokenAsync())
            navigation.NavigateTo("/login", true);
    }
}