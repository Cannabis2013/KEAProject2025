using ALBackend.DataTransferObject.Forum;
using ALBackend.Entities.Forum;
using ALBackend.Entities.Identity;
using ALMembers.Entities;

namespace ALBackend.Services.Forum;

public interface ITopicFetcher
{
    public List<TopicCard> TopicsWithoutPosts(int pageIndex,int pageSize, Member currentMember);
    public TopicCard? Topic(int topicId,Member currentMember);
}