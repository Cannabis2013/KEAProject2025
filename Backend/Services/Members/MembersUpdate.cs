using ALBackend.DataTransferObject.Members;
using ALBackend.Persistence.Members;
using ALBackend.Services.Identity.Users;

namespace ALBackend.Services.Members;

public class MembersUpdate(MembersDb membersDb, 
    IUsersUpdate usersUpdate) : IMembersUpdate
{
    public async Task<bool> Create(MemberInfo request)
    {
        var member = request.ToMember();
        member.UserId = await usersUpdate.CreateAsync(request.ToCredentials());
        membersDb.Add(member);
        return await membersDb.SaveChangesAsync() > 0;
    }

    public async Task<bool> Update(MemberInfo request)
    {
        await usersUpdate.UpdateAsync(request.ToCredentials());
        var member = await membersDb.Members.FindAsync(request.MemberId);
        if(member is null) return false;
        member.FirstName = request.FirstName;
        member.LastName = request.LastName;
        member.Title = request.Title;
        membersDb.Update(member);
        return await membersDb.SaveChangesAsync() > 0;
    }

    public async Task<bool> RemoveAsync(int memberId)
    {
        var member = await membersDb.Members.FindAsync(memberId);
        if (member is null) return false;
        await usersUpdate.RemoveAsync(member.UserId);
        membersDb.Remove(memberId);
        return await membersDb.SaveChangesAsync() > 0;
    }
}