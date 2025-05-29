using System.Security.Claims;
using ALBackend.DataTransferObject.Members;
using ALBackend.Entities.Identity;
using ALBackend.Persistence;
using ALMembers.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ALBackend.Services.Members;

public class MembersFetcher(UserManager<UserAccount> userManager,MariaDbContext dbContext) : IMembersFetcher
{
    public MemberFetchResponse? One(int memberId)
    {
        var member = dbContext.Members
            .Include(u => u.Images)
            .FirstOrDefault(member => member.Id == memberId);
        if (member is null) return null;
        return new(member);
    }

    public Member? One(ClaimsPrincipal principal)
    {
        var authenticated = principal?.Identity?.IsAuthenticated ?? false;
        if (principal is null || !authenticated) return null;
        
        var userId = principal.FindFirst(claim =>  claim.Type == "Id" )?.Value;
        var user = userManager.Users.FirstOrDefault(u => u.Id == userId);
        if (user is null) return null;

        var member = dbContext
            .Members
            .FirstOrDefault(member => member.UserId == Guid.Parse(user.Id));

        return member;
    }

    public MemberFetchResponse? One(Guid userId)
    {
        var member = dbContext.Members
            .Include(u => u.Images)
            .FirstOrDefault(member => member.UserId == userId);
        if (member is null) return null;
        return new(member);
    }

    public List<MemberFetchResponse> Many()
    {
        return dbContext.Members
            .AsEnumerable()
            .Select(member => new MemberFetchResponse(member))
            .ToList();
    }
}