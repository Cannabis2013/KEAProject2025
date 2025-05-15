using ALBackend.DataTransferObject.Forum;
using ALBackend.Entities.Forum;
using ALBackend.Persistence.Forum;
using ALMembers.Entities;

namespace ALBackend.Services.Forum;

public class PostUpdater(ForumDb forumDb) : IPostUpdater
{
    public async Task<int> AddPost(PostUpdateRequest request, Member currentMember)
    {
        var post = new Post
        {
            memberId = currentMember.Id,
            Content = request.Message,
            TopicId = request.TopicId
        };
        forumDb.Posts.Add(post);
        await forumDb.SaveChangesAsync();
        return post.Id;
    }

    public async Task<bool> UpdatePost(PostUpdateRequest request)
    {
        var post = await forumDb.Posts.FindAsync(request.Id);
        if (post is null) return false;
        post.Content = request.Message;
        return await forumDb.SaveChangesAsync() > 0;
    }

    public async Task<bool> RemovePost(int topicId)
    {
        var post = await forumDb.Posts.FindAsync(topicId);
        if (post is null) return false;
        forumDb.Posts.Remove(post);
        return await forumDb.SaveChangesAsync() > 0;
    }
}