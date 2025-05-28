using System.Security.Claims;
using ALBackend.DataTransferObject.Members;
using ALMembers.Entities;

namespace ALBackend.Services.Members;

public interface IMembersFetcher
{
    public MemberFetchResponse? One(int memberId);
    public Member? One(ClaimsPrincipal principal);
    public MemberFetchResponse? One(Guid userId);
    public List<MemberFetchResponse> Many();
}