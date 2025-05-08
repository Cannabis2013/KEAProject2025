namespace ALAdmin.DataTransferObjects.Auth;

public class AuthRefreshRequest(string refreshToken, string email)
{
    public string RefreshToken { get; init; } = refreshToken;
    public string Email { get; init; } = email;
}