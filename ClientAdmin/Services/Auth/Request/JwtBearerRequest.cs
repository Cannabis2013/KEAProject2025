using ALAdmin.Services.Auth.Storage;

namespace ALAdmin.Services.Auth.Request;

public class JwtBearerRequest(IAuthStorage storage, HttpClient client) : IAuthRequest
{
    private async Task AddTokenAuthorization(HttpClient httpClient)
    {
        var token = await storage.AccessToken();
        httpClient.DefaultRequestHeaders.Remove("Authorization");
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
    }

    private async Task<HttpResponseMessage> _get(string url)
    {
        await AddTokenAuthorization(client);
        return await client.GetAsync(url);
    }

    public async Task<TResponse?> GetAsync<TResponse>(string url)
    {
        var res = await _get(url);
        if (!res.IsSuccessStatusCode) return default;
        return await res.Content.ReadFromJsonAsync<TResponse>();
    }

    public Task<HttpResponseMessage> GetAsync(string url)
        => _get(url);

    public async Task<bool> PostAsync(string url, object data)
    {
        await AddTokenAuthorization(client);
        return (await client.PostAsJsonAsync(url, data)).IsSuccessStatusCode;
    }

    public async Task<bool> PatchAsync(string url, object data)
    {
        await AddTokenAuthorization(client);
        return (await client.PatchAsJsonAsync(url, data)).IsSuccessStatusCode;
    }

    public async Task<bool> RemoveAsync(string url)
    {
        await AddTokenAuthorization(client);
        return (await client.DeleteAsync(url)).IsSuccessStatusCode;
    }
}