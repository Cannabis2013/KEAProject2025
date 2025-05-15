using ALBackend.DataTransferObject.Forum;
using ALMembers.Entities;

namespace ALBackend.Services.Forum;

public interface IPostUpdater
{
    public Task<int> AddPost(PostUpdateRequest request,Member currentMember);
    public Task<bool> UpdatePost(PostUpdateRequest request);
    public Task<bool> RemovePost(int topicId);
}