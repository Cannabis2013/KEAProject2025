using ALBackend.DataTransferObject.Forum;
using ALBackend.Entities.Forum;
using ALBackend.Persistence;
using ALMembers.Entities;

namespace ALBackend.Services.Forum;

public class TopicFetcher(MariaDbContext dbContext) : ITopicFetcher
{
    private TopicFetchResponse ToFetchResponse(Topic topic, Member currentMember)
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
        
        return new(topic, topicCreator)
        {
            IsOwner = topic.memberId == currentMember.Id,
            PostsCount = postsCount,
            LastPoster = $"{lastPoster?.FirstName} {lastPoster?.LastName}",
            InitialMessage = topic.InitialMessage
        };
    }

    public List<TopicFetchResponse> TopicsWithoutPosts(int pageIndex, int pageSize, Member currentMember)
    {
        var topics = dbContext
            .Topics
            .OrderByDescending(topic => topic.CreatedAt)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToList();

        return topics
            .Select(topic => ToFetchResponse(topic, currentMember))
            .ToList();
    }

    public TopicFetchResponse? Topic(int topicId, Member currentMember)
    {
        var topic = dbContext
            .Topics
            .Find(topicId);

        return topic is null ? null : ToFetchResponse(topic, currentMember);
    }

    public List<TopicFetchResponse> RecentlyActive(int count)
    {
        var active = dbContext.Topics
            .ToList()
            .Where(topic => dbContext.Posts.Any(post => post.TopicId == topic.Id))
            .Select(topic =>
            {
                var lastPost = dbContext
                    .Posts
                    .ToList()
                    .Last(post => post.TopicId == topic.Id);
                
                var topicCreator = dbContext.Members.Find(topic.memberId);
                
                return new TopicFetchResponse(topic,topicCreator)
                {
                    LastPoster = $"{topicCreator?.FirstName} {topicCreator?.LastName}",
                    LastPostedOn = lastPost.CreatedAt
                };
            })
            .OrderByDescending(response => response.LastPostedOn)
            .Take(count)
            .ToList();
        
        return active;
    }
}