using ALBackend.DataTransferObject.Members;

namespace ALBackend.Services.Members;

public interface IMembersUpdate
{
    public Task<bool> Create(MemberInfo request);
    public Task<bool> Update(MemberInfo request);
    public Task<bool> RemoveAsync(int memberId);
}