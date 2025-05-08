namespace ALAdmin.Services.Auth.Authentication;

public interface IAuthentication
{
    public Task AuthenticateAsync();
}