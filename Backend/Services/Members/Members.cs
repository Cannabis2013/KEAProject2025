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
    public Member? One(int id)
    {
        return dbContext.Members
            .Include(u => u.ProfileImages)
            .FirstOrDefault(member => member.Id == id);
    }

    public Member? One(ClaimsPrincipal principal)
    {
        var authenticated = principal?.Identity?.IsAuthenticated ?? false;
        if (principal is null || !authenticated) return null;

        var userId = principal.FindFirst(claim => claim.Type == "Id")?.Value;
        var user = userManager.Users.FirstOrDefault(u => u.Id == userId);
        if (user is null) return null;

        return dbContext
            .Members
            .FirstOrDefault(member => member.UserId == Guid.Parse(user.Id));
    }

    public Member? One(Guid userId)
    {
        return dbContext.Members
            .Include(u => u.ProfileImages)
            .FirstOrDefault(member => member.UserId == userId);
    }

    public async Task<List<Member>> ManyAsync() =>
        await dbContext.Members.ToListAsync();

    public async Task<bool> Create(MemberUpdateRequest request)
    {
        var member = request.ToMember();
        member.UserId = await usersUpdate.CreateAsync(request.ToCredentials());
        dbContext.Members.Add(member);
        return await dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> Update(MemberUpdateRequest request)
    {
        await usersUpdate.UpdateAsync(request.ToCredentials());
        var member = await dbContext.Members.FindAsync(request.MemberId);
        if (member is null) return false;
        var updated = request.ToUpdated(member);
        dbContext.Update(updated);
        return await dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> RemoveAsync(int id)
    {
        var member = await dbContext.Members.FindAsync(id);
        if (member is null) return false;
        await usersUpdate.RemoveAsync(member.UserId);
        dbContext.Remove(id);
        return await dbContext.SaveChangesAsync() > 0;
    }
}