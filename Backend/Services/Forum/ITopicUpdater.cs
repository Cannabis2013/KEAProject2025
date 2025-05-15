using ALBackend.DataTransferObject.Forum;
using ALBackend.Entities.Forum;
using ALBackend.Persistence.Forum;
using ALBackend.Persistence.Members;
using ALMembers.Entities;

namespace ALBackend.Services.Forum;

public interface ITopicUpdater
{
    public Task<int> AddTopic(TopicUpdateRequest request, Member currentMember);
    public Task<bool> UpdateTopic(TopicUpdateRequest request);
}