using ALBackend.DataTransferObject.Forum;
using ALMembers.Entities;

namespace ALBackend.Services.Forum;

public interface IPostFetcher
{
    public List<PostCard> Posts(int pageIndex,int pageSize, int topicId, Member currentMember);
    public PostUpdateCard? Post(int id);
}