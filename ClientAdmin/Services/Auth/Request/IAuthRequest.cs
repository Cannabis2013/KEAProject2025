namespace ALAdmin.Services.Auth.Request;

public interface IAuthRequest
{
    public Task<TResponse?> GetAsync<TResponse>(string url);
    public Task<HttpResponseMessage> GetAsync(string url);
    public Task<bool> PostAsync(string url, object data);
    public Task<bool> PatchAsync(string url, object data);
    public Task<bool> RemoveAsync(string url);
}