using ALBackend.DataTransferObject.Members;
using Microsoft.AspNetCore.Mvc;

namespace ALBackend.Services.Members;

public interface IMembersFetcher
{
    public MemberFetchResponse? One(int memberId);
    public MemberFetchResponse? One(Guid userId);
    public List<MemberFetchResponse> Many();
}