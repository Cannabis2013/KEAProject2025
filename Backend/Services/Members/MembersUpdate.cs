using ALBackend.DataTransferObject.Members;
using ALBackend.Persistence;
using ALBackend.Services.Identity.Users;

namespace ALBackend.Services.Members;

public class MembersUpdate(MariaDbContext dbContext, 
    IUsersUpdate usersUpdate) : IMembersUpdate
{
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
        if(member is null) return false;
        member.FirstName = request.FirstName;
        member.LastName = request.LastName;
        member.Title = request.Title;
        dbContext.Update(member);
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