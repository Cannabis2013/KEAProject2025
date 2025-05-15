using ALBackend.DataTransferObject.Forum;
using ALBackend.Entities.Forum;
using ALBackend.Persistence.Forum;
using ALBackend.Persistence.Members;
using ALMembers.Entities;

namespace ALBackend.Services.Forum;

public class ForumFetcher(ForumDb forumDb, MembersDb membersDb) : IForumFetcher
{
    private TopicCard ToCard(Topic topic, Member currentMember, bool withPosts = false)
    {
        var posts = forumDb
            .Posts
            .Where(post => post.TopicId == topic.Id)
            .OrderBy(post => post.CreatedAt)
            .ToList();

        var postsCount = posts.Count;
        var lastPost = postsCount > 0 ? posts.Last() : null;
        var lastPoster = membersDb.Members.Find(lastPost?.memberId);
        var topicCreator = membersDb.Members.Find(topic.memberId);
        if (topicCreator is null) throw new BadHttpRequestException("Member not found");
        
        return new(topic, topicCreator)
        {
            IsOwner = topic.memberId == currentMember.Id,
            PostsCount = postsCount,
            lastPoster = $"{lastPoster?.FirstName} {lastPoster?.LastName}",
            InitialMessage = topic.InitialMessage
        };
    }

    public List<TopicCard> ManyWithoutPosts(int pageIndex, int pageSize, Member currentMember)
    {
        var topics = forumDb
            .Topics
            .OrderByDescending(topic => topic.CreatedAt)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToList();

        return topics
            .Select(topic => ToCard(topic, currentMember))
            .ToList();
    }

    public TopicCard? One(int topicId, Member currentMember)
    {
        var topic = forumDb
            .Topics
            .Find(topicId);

        return topic is null ? null : ToCard(topic, currentMember, true);
    }

    public List<PostCard> Posts(int pageIndex, int pageSize, int topicId, Member currentMember)
    {
        return forumDb.Posts
            .Where(post => post.TopicId == topicId)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToList()
            .Select(post =>
            {
                var author = membersDb.Members.Find(post.memberId);
                return new PostCard(post)
                {
                    IsOwner = post.memberId == currentMember.Id,
                    Author = $"{author?.FirstName} {author?.LastName}"
                };
            })
            .ToList();
    }
}