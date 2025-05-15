using ALBackend.DataTransferObject.Forum;
using ALBackend.Entities.Forum;
using ALMembers.Entities;

namespace ALBackend.Services.Forum;

public interface IForumUpdater
{
    public Task<int> AddTopic(TopicAddRequest request, Member currentMember);
    public Task<int> AddPost(PostAddRequest request,Member currentMember);
}