using ALBackend.DataTransferObject.Forum;
using ALMembers.Entities;

namespace ALBackend.Services.Forum;

public interface IPostFetcher
{
    public List<PostFetchRequest> AsBlocks(int pageIndex,int pageSize, int topicId, Member currentMember);
    public List<PostFetchRequest> AsCards(int count);
    public PostFetchRequest? Post(int id);
}