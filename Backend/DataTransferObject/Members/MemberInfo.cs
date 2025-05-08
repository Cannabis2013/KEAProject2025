using ALBackend.Entities.Identity;
using ALMembers.Entities;

namespace ALBackend.DataTransferObject.Members;

public class MemberInfo
{
    public required int MemberId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required List<string> Roles { get; set; }
    public string? Password { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Title { get; set; }
    public UserCredentials ToCredentials()
    {
        return new()
        {
            UserName = Email,
            Password = Password!,
            Email = Email,
            Roles = Roles,
            PhoneNumber = PhoneNumber
        };
    }

    public Member ToMember()
    {
        return new()
        {
            FirstName = FirstName,
            LastName = LastName,
            Id = MemberId,
            Title = Title
        };
    }
}