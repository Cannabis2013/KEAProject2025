using ALBackend.DataTransferObject.Forum;
using ALBackend.Persistence;
using ALMembers.Entities;

namespace ALBackend.Services.Forum;

public class TopicUpdater(MariaDbContext dbContext) : ITopicUpdater
{
    public async Task<int> AddTopic(TopicUpdateRequest request, Member currentMember)
    {
        var topic = request.ToEntity(currentMember.Id);
        dbContext.Topics.Add(topic);
        await dbContext.SaveChangesAsync();
        return topic.Id;
    }

    public async Task<bool> UpdateTopic(TopicUpdateRequest request, Member currentMember)
    {
        var topic = await dbContext.Topics.FindAsync(request.TopicId);
        if (topic is null) return false;
        if (currentMember.Id != topic.memberId) return false;
        request.ToUpdateEntity(topic);
        return await dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> RemoveTopic(int topicId, Member currentMember)
    {
        var topic = await dbContext.Topics.FindAsync(topicId);
        if (topic is null) return false;
        if (currentMember.Id != topic.memberId) return false;
        var posts = dbContext.Posts
            .Where(post => post.TopicId == topicId)
            .ToList();
        dbContext.Posts.RemoveRange(posts);
        dbContext.Topics.Remove(topic);
        return await dbContext.SaveChangesAsync() > 0;
    }
}