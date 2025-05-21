using ALBackend.DataTransferObject.Forum;
using ALBackend.Entities.Forum;
using ALBackend.Persistence.Forum;
using ALMembers.Entities;

namespace ALBackend.Services.Forum;

public class TopicUpdater(ForumDb forumDb) : ITopicUpdater
{
    public async Task<int> AddTopic(TopicUpdateRequest request, Member currentMember)
    {
        var topic = request.ToEntity(currentMember.Id);
        forumDb.Topics.Add(topic);
        await forumDb.SaveChangesAsync();
        return topic.Id;
    }

    public async Task<bool> UpdateTopic(TopicUpdateRequest request, Member currentMember)
    {
        var topic = await forumDb.Topics.FindAsync(request.TopicId);
        if (topic is null) return false;
        if (currentMember.Id != topic.memberId) return false;
        request.ToUpdateEntity(topic);
        return await forumDb.SaveChangesAsync() > 0;
    }

    public async Task<bool> RemoveTopic(int topicId, Member currentMember)
    {
        var topic = await forumDb.Topics.FindAsync(topicId);
        if (topic is null) return false;
        if (currentMember.Id != topic.memberId) return false;
        var posts = forumDb.Posts
            .Where(post => post.TopicId == topicId)
            .ToList();
        forumDb.Posts.RemoveRange(posts);
        forumDb.Topics.Remove(topic);
        return await forumDb.SaveChangesAsync() > 0;
    }
}