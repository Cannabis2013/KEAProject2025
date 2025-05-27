using ALBackend.DataTransferObject.Forum;
using ALBackend.Persistence.Forum;
using ALBackend.Persistence.Members;
using ALMembers.Entities;

namespace ALBackend.Services.Forum;

public class PostFetcher(ForumDb forumDb,MembersDb membersDb) : IPostFetcher
{
    public List<PostFetchRequest> AsBlocks(int pageIndex, int pageSize, int topicId, Member currentMember)
    {
        return forumDb.Posts
            .Where(post => post.TopicId == topicId)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToList()
            .Select(post =>
            {
                var author = membersDb.Members.Find(post.memberId);
                return new PostFetchRequest(post)
                {
                    IsOwner = post.memberId == currentMember.Id,
                    Author = $"{author?.FirstName} {author?.LastName}"
                };
            })
            .ToList();
    }

    public List<PostFetchRequest> AsCards(int count)
    {
        return forumDb.Posts
            .OrderByDescending(post => post.CreatedAt)
            .Take(count)
            .Select(post => new PostFetchRequest(post))
            .ToList();
    }

    public PostFetchRequest? Post(int id)
    {
        var post = forumDb.Posts.Find(id);
        return post is null ? null : new(post);
    }
}