namespace ALBackend.Entities.Identity;

// ReSharper disable once ClassNeverInstantiated.Global
public class UserCredentials
{
    public required string UserName { get; init; }
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required List<string> Roles { get; set; }
    public required string PhoneNumber { get; set; }

    public UserAccount ToUser()
    {
        return new()
        {
            UserName = UserName,
            Email = Email,
            PhoneNumber = PhoneNumber
        };
    }
}