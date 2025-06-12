using System.Security.Claims;
using ALBackend.DataTransferObject.Members;
using ALMembers.Entities;

namespace ALBackend.Services.Members;

public interface IMembers
{
    public Member? One(int id);
    public Member? One(ClaimsPrincipal principal);
    public Member? One(Guid userId);
    public Task<List<Member>> ManyAsync();
    public Task<bool> Create(MemberUpdateRequest request);
    public Task<bool> Update(MemberUpdateRequest request);
    public Task<bool> RemoveAsync(int id);
}