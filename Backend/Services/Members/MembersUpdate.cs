using ALBackend.DataTransferObject.Members;
using ALBackend.Persistence.Members;
using ALBackend.Services.Identity.Users;

namespace ALBackend.Services.Members;

public class MembersUpdate(MembersDb membersDb, 
    IUsersUpdate usersUpdate) : IMembersUpdate
{
    public async Task Create(MemberInfo request)
    {
        var member = request.ToMember();
        member.UserId = await usersUpdate.CreateAsync(request.ToCredentials());
        membersDb.Add(member);
        await membersDb.SaveChangesAsync();
    }

    public async Task Update(MemberInfo request)
    {
        await usersUpdate.UpdateAsync(request.ToCredentials());
        var member = await membersDb.Members.FindAsync(request.MemberId);
        if(member is null) return;
        member.FirstName = request.FirstName;
        member.LastName = request.LastName;
        member.Title = request.Title;
        membersDb.Update(member);
        await membersDb.SaveChangesAsync();
    }

    public async Task RemoveAsync(int memberId)
    {
        var member = await membersDb.Members.FindAsync(memberId);
        if (member is null) return;
        await usersUpdate.RemoveAsync(member.UserId);
        membersDb.Remove(memberId);
        await membersDb.SaveChangesAsync();
    }
}