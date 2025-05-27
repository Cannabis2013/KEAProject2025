using ALBackend.DataTransferObject.Forum;
using ALBackend.Entities.Forum;
using ALBackend.Entities.Identity;
using ALMembers.Entities;

namespace ALBackend.Services.Forum;

public interface ITopicFetcher
{
    public List<TopicFetchResponse> TopicsWithoutPosts(int pageIndex,int pageSize, Member currentMember);
    public TopicFetchResponse? Topic(int topicId,Member currentMember);
    public List<TopicFetchResponse> RecentlyActive(int count);
}