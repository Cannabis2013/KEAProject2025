using ALBackend.DataTransferObject.Forum;
using ALBackend.Entities.Forum;
using ALBackend.Persistence;
using ALMembers.Entities;

namespace ALBackend.Services.Forum;

public class PostUpdater(MariaDbContext dbContext) : IPostUpdater
{
    public async Task<int> AddPost(PostUpdateRequest request, Member currentMember)
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

    public async Task<bool> UpdatePost(PostUpdateRequest request)
    {
        var post = await dbContext.Posts.FindAsync(request.Id);
        if (post is null) return false;
        post.Content = request.Message;
        return await dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> RemovePost(int topicId)
    {
        var post = await dbContext.Posts.FindAsync(topicId);
        if (post is null) return false;
        dbContext.Posts.Remove(post);
        return await dbContext.SaveChangesAsync() > 0;
    }
}