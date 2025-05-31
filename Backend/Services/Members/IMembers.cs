using System.Security.Claims;
using ALBackend.DataTransferObject.Members;
using ALMembers.Entities;

namespace ALBackend.Services.Members;

public interface IMembers
{
    public Member? One(int memberId);
    public Member? One(ClaimsPrincipal principal);
    public Member? One(Guid userId);
    public List<MemberFetchResponse> Many();
    public Task<bool> Create(MemberInfo request);
    public Task<bool> Update(MemberInfo request);
    public Task<bool> RemoveAsync(int memberId);
}