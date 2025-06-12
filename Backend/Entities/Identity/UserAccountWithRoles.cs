namespace ALBackend.Entities.Identity;

public class UserAccountWithRoles
{
    public required UserAccount User { get; set; }
    public required List<string> Roles { get; init; }
}