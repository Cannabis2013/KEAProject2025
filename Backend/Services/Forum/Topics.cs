using ALBackend.DataTransferObject.Forum;
using ALBackend.Entities.Forum;
using ALBackend.Persistence;
using ALMembers.Entities;
using Microsoft.EntityFrameworkCore;

namespace ALBackend.Services.Forum;

public class Topics(MariaDbContext dbContext) : ITopics
{
    public async Task<List<Topic>> ManyAsync(int pageIndex, int pageSize, Member currentMember)
    {
        return await dbContext
            .Topics
            .OrderByDescending(topic => topic.CreatedAt)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Topic?> OneAsync(int topicId)
    {
        return await dbContext
            .Topics
            .FindAsync(topicId);
    }

    public async Task<List<Topic>> RecentlyActiveAsync(int count)
    {
        var topics = await dbContext
            .Topics
            .ToListAsync();
        var active = topics
            .Where(topic => dbContext.Posts.Any(p => p.TopicId == topic.Id))
            .OrderByDescending(topic =>
            {
                var posts = dbContext
                    .Posts
                    .Where(post => post.TopicId == topic.Id)
                    .OrderByDescending(post => post.CreatedAt)
                    .First();
                return posts.CreatedAt;
            })
            .Take(count)
            .ToList();

        return active;
    }

    public async Task<int> AddAsync(TopicUpdateRequest request, Member currentMember)
    {
        var topic = request.ToEntity(currentMember.Id);
        dbContext.Topics.Add(topic);
        await dbContext.SaveChangesAsync();
        return topic.Id;
    }

    public async Task<bool> UpdateAsync(TopicUpdateRequest request, Member currentMember)
    {
        var topic = await dbContext.Topics.FindAsync(request.TopicId);
        if (topic is null) return false;
        if (currentMember.Id != topic.memberId) return false;
        request.ToUpdateEntity(topic);
        return await dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> RemoveAsync(int topicId, Member currentMember)
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