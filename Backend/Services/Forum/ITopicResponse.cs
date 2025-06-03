using ALBackend.DataTransferObject.Forum;
using ALBackend.Entities.Forum;
using ALMembers.Entities;

namespace ALBackend.Services.Forum;

public interface ITopicResponse
{
    public TopicFetchResponse ResponseWithPosterInfo(Topic topic, Member currentMember);
}