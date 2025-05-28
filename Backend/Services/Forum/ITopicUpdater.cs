using ALBackend.DataTransferObject.Forum;
using ALMembers.Entities;

namespace ALBackend.Services.Forum;

public interface ITopicUpdater
{
    public Task<int> AddTopic(TopicUpdateRequest request, Member currentMember);
    public Task<bool> UpdateTopic(TopicUpdateRequest request, Member currentMember);
    public Task<bool> RemoveTopic(int topicId, Member currentMember);
}