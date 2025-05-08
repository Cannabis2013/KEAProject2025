namespace ALAdmin.DataTransferObjects.Auth;

public class AuthRefreshResponse
{
    public string AccessToken { get; set; } = "";
    public string RefreshToken { get; set; } = "";
}