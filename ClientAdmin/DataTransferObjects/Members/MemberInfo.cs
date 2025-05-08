using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace ALAdmin.DataTransferObjects.Members;

public class MemberInfo
{
    public int MemberId { get; set; } = 0;
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Email { get; set; } = "";
    public string PhoneNumber { get; set; } = "";
    
    public string Password { get; set; } = "";
    public string Title { get; set; } = "";
    public List<string> Roles { get; set; } = [];

    public MemberInfo ToDuplicated()
    {
        return new()
        {
            MemberId = MemberId,
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            PhoneNumber = PhoneNumber,
            Password = Password,
            Roles = Roles,
            Title = Title.Length != 0 ? Title : "Intet"
        };
    }

    public object ToDto()
    {
        return new
        {
            MemberId,
            FirstName,
            LastName,
            Password,
            PhoneNumber,
            Email,
            Roles,
            Title = Title.Length != 0 ? Title : "Intet"
        };
    }
}