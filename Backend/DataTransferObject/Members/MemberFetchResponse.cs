using ALBackend.Entities.Identity;
using ALBackend.Entities.Members;
using ALMembers.Entities;

namespace ALBackend.DataTransferObject.Members;

public class MemberFetchResponse(Member member, UserAccount? user = null, IList<string>? roles = null)
{
    public int MemberId { get; set; } = member.Id;
    public string FirstName { get; set; } = member.FirstName;
    public string LastName { get; set; } = member.LastName;
    public string? Email { get; set; } = user?.Email ?? "";
    public DateTime JoinedAt { get; set; } = member.JoinedDate;
    public ICollection<ProfileImage> Images = member.ProfileImages;
    public IList<string>? Roles { get; set; } = roles;
    public Guid UserId { get; set; } = Guid.Parse(user?.Id ?? "");
    public string Title { get; set; } = member.Title;
    public string? ProfileImageAsBase64 { get; set; }
    public string PhoneNumber { get; set; } = user?.PhoneNumber ?? "";
}