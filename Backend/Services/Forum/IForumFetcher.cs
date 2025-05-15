using ALBackend.DataTransferObject.Forum;
using ALBackend.Entities.Forum;
using ALBackend.Entities.Identity;
using ALMembers.Entities;

namespace ALBackend.Services.Forum;

public interface IForumFetcher
{
    public List<TopicCard> ManyWithoutPosts(int pageIndex,int pageSize, Member currentMember);
    public TopicCard? One(int topicId,Member currentMember);
    public List<PostCard> Posts(int pageIndex,int pageSize, int topicId, Member currentMember);
}