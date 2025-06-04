using ALBackend.DataTransferObject.Forum;
using ALBackend.Entities.Forum;
using ALMembers.Entities;

namespace ALBackend.Services.Forum;

public interface IPosts
{
    public Task<List<Post>> Paginated(int pageIndex,int pageSize, int topicId, Member currentMember);
    public Task<Post?> OneAsync(int id);
    public Task<int> AddAsync(PostUpdateRequest request,Member currentMember);
    public Task<bool> UpdateAsync(PostUpdateRequest request);
    public Task<bool> RemoveAsync(int topicId);
}