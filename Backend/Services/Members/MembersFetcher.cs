using ALBackend.DataTransferObject.Members;
using ALBackend.Persistence.Identity;
using ALBackend.Persistence.Members;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ALBackend.Services.Members;

public class MembersFetcher(MembersDb members) : IMembersFetcher
{
    public MemberFetchResponse? One(int memberId)
    {
        var member = members.Members
            .Include(u => u.Images)
            .FirstOrDefault(member => member.Id == memberId);
        if (member is null) return null;
        return new(member);
    }

    public MemberFetchResponse? One(Guid userId)
    {
        var member = members.Members
            .Include(u => u.Images)
            .FirstOrDefault(member => member.UserId == userId);
        if (member is null) return null;
        return new(member);
    }

    public List<MemberFetchResponse> Many()
    {
        return members.Members
            .AsEnumerable()
            .Select(member => new MemberFetchResponse(member))
            .ToList();
    }
}