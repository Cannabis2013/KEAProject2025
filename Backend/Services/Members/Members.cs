using System.Security.Claims;
using ALBackend.DataTransferObject.Members;
using ALBackend.Entities.Identity;
using ALBackend.Persistence;
using ALBackend.Services.Identity.Users;
using ALMembers.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ALBackend.Services.Members;

public class Members(
    UserManager<UserAccount> userManager,
    MariaDbContext dbContext,
    IUsersUpdate usersUpdate) : IMembers
{
    public Member? One(int memberId)
    {
        return dbContext.Members
            .Include(u => u.ProfileImages)
            .FirstOrDefault(member => member.Id == memberId);
    }

    public Member? One(ClaimsPrincipal principal)
    {
        var authenticated = principal?.Identity?.IsAuthenticated ?? false;
        if (principal is null || !authenticated) return null;

        var userId = principal.FindFirst(claim => claim.Type == "Id")?.Value;
        var user = userManager.Users.FirstOrDefault(u => u.Id == userId);
        if (user is null) return null;

        var member = dbContext
            .Members
            .FirstOrDefault(member => member.UserId == Guid.Parse(user.Id));

        return member;
    }

    public Member? One(Guid userId)
    {
        return dbContext.Members
            .Include(u => u.ProfileImages)
            .FirstOrDefault(member => member.UserId == userId);
    }

    public List<MemberFetchResponse> Many()
    {
        return dbContext.Members
            .AsEnumerable()
            .Select(member => new MemberFetchResponse(member))
            .ToList();
    }

    public async Task<bool> Create(MemberInfo request)
    {
        var member = request.ToMember();
        member.UserId = await usersUpdate.CreateAsync(request.ToCredentials());
        dbContext.Members.Add(member);
        return await dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> Update(MemberInfo request)
    {
        await usersUpdate.UpdateAsync(request.ToCredentials());
        var member = await dbContext.Members.FindAsync(request.MemberId);
        if (member is null) return false;
        var updated = request.ToUpdated(member);
        dbContext.Update(updated);
        return await dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> RemoveAsync(int memberId)
    {
        var member = await dbContext.Members.FindAsync(memberId);
        if (member is null) return false;
        await usersUpdate.RemoveAsync(member.UserId);
        dbContext.Remove(memberId);
        return await dbContext.SaveChangesAsync() > 0;
    }
}