namespace ALBackend.Entities.Identity;

// ReSharper disable once ClassNeverInstantiated.Global
public class JwtCredentials
{
    public string Email { get; set; } = "";
    public string? RefreshToken { get; set; }
}