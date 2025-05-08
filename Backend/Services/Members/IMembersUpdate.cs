using ALBackend.DataTransferObject.Members;

namespace ALBackend.Services.Members;

public interface IMembersUpdate
{
    public Task Create(MemberInfo request);
    public Task Update(MemberInfo request);
    public Task RemoveAsync(int memberId);
}