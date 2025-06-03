using ALBackend.DataTransferObject.Forum;
using ALBackend.Entities.Forum;
using ALBackend.Persistence;
using ALMembers.Entities;

namespace ALBackend.Services.Forum;

public class TopicResponse(MariaDbContext dbContext) : ITopicResponse
{
    public TopicFetchResponse ResponseWithPosterInfo(Topic topic, Member currentMember)
    {
        var posts = dbContext
            .Posts
            .Where(post => post.TopicId == topic.Id)
            .OrderBy(post => post.CreatedAt)
            .ToList();

        var postsCount = posts.Count;
        var lastPost = postsCount > 0 ? posts.Last() : null;
        var lastPoster = dbContext.Members.Find(lastPost?.memberId);
        var topicCreator = dbContext.Members.Find(topic.memberId);
        if (topicCreator is null) throw new BadHttpRequestException("Member not found");

        return new(topic, topicCreator,currentMember)
        {
            PostsCount = postsCount,
            LastPoster = $"{lastPoster?.FirstName} {lastPoster?.LastName}",
            InitialMessage = topic.InitialMessage
        };
    }
}