using Microsoft.AspNetCore.Identity;

namespace ALBackend.Entities.Identity;

// ReSharper disable once ClassNeverInstantiated.Global
public class UserAccount : IdentityUser
{
    public string RefreshToken { get; set; } = "";

    public object ToFullDto(IList<string> roles)
    {
        return new
        {
            UserName,
            Email,
            Id,
            Roles = roles
        };
    }

    public object ToListDto()
    {
        return new
        {
            Email,
            Id,
            UserName
        };
    }
}