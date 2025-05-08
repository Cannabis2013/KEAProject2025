using ALBackend.DataTransferObject.Members;
using ALBackend.Entities.Identity;
using ALBackend.Persistence.Identity;
using ALBackend.Persistence.Members;
using ALMembers.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ALBackend.Services.Members;

public class MembersFetch(MembersDb members, IdentityDb users, UserManager<UserAccount> userManager) : IMembersFetch
{
    public async Task<JsonResult> One(int memberId)
    {
        var member = members.Members
            .Include(u => u.Images)
            .FirstOrDefault(member => member.Id == memberId);
        if(member is null) return new("Member not found."){ StatusCode = StatusCodes.Status404NotFound};
        var user = await userManager.FindByIdAsync(member.UserId.ToString());
        if(user is null) return new("User not found"){ StatusCode = StatusCodes.Status404NotFound};
        var roles = await userManager.GetRolesAsync(user);
        return new(new MemberDetailedResponse(member,user,roles)){StatusCode = StatusCodes.Status200OK};
    }

    public async Task<JsonResult> One(Guid memberId)
    {
        var member = members.Members
            .Include(u => u.Images)
            .FirstOrDefault(member => member.UserId == memberId);
        if(member is null) return new("Member not found."){ StatusCode = StatusCodes.Status404NotFound};
        var user = await userManager.FindByIdAsync(member.UserId.ToString());
        if(user is null) return new("No user associated with member"){ StatusCode = StatusCodes.Status404NotFound};
        var roles = await userManager.GetRolesAsync(user);
        return new(new MemberDetailedResponse(member,user,roles)){StatusCode = StatusCodes.Status200OK};
    }

    public JsonResult All()
    {
        var result = members.Members
            .AsEnumerable()
            .Select(member =>
            {
                var user = users.Users.Find(member.UserId.ToString());
                return user is null ? null : MinimalMember(member, user);
            })
            .Where(user => user is not null)
            .ToList();
        return new(result);
    }

    private static object MinimalMember(Member member, UserAccount user)
    {
        return new
        {
            MemberId = member.Id,
            member.FirstName,
            member.LastName,
            user.Email,
            member.LastPayment
        };
    }
}