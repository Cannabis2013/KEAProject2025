namespace ALAdmin.DataTransferObjects.Auth;

// ReSharper disable once ClassNeverInstantiated.Global
public class AuthLoginResponse
{
    public string AccessToken { get; set; } = "";
    public string RefreshToken { get; set; } = "";
    public Guid Id { get; set; } = Guid.Empty;
    public string Email { get; set; } = "";
}