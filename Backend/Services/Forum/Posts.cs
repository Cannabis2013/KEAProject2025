using ALBackend.DataTransferObject.Forum;
using ALBackend.Entities.Forum;
using ALBackend.Persistence;
using ALMembers.Entities;
using Microsoft.EntityFrameworkCore;

namespace ALBackend.Services.Forum;

public class Posts(MariaDbContext dbContext) : IPosts
{
    public async Task<List<Post>> Paginated(int pageIndex, int pageSize, int topicId, Member currentMember)
    {
        return await dbContext.Posts
            .Where(post => post.TopicId == topicId)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Post?> OneAsync(int id) => await dbContext.Posts.FindAsync(id);

    public async Task<int> AddAsync(PostUpdateRequest request, Member currentMember)
    {
        var post = new Post
        {
            memberId = currentMember.Id,
            Content = request.Message,
            TopicId = request.TopicId
        };
        dbContext.Posts.Add(post);
        await dbContext.SaveChangesAsync();
        return post.Id;
    }

    public async Task<bool> UpdateAsync(PostUpdateRequest request)
    {
        var post = await dbContext.Posts.FindAsync(request.Id);
        if (post is null) return false;
        post.Content = request.Message;
        return await dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> RemoveAsync(int topicId)
    {
        var post = await dbContext.Posts.FindAsync(topicId);
        if (post is null) return false;
        dbContext.Posts.Remove(post);
        return await dbContext.SaveChangesAsync() > 0;
    }
}