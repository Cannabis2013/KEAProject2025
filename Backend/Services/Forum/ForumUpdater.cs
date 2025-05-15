using ALBackend.DataTransferObject.Forum;
using ALBackend.Entities.Forum;
using ALBackend.Persistence.Forum;
using ALBackend.Persistence.Members;
using ALMembers.Entities;

namespace ALBackend.Services.Forum;

public class ForumUpdater(ForumDb forumDb) : IForumUpdater
{
    public async Task<int> AddTopic(TopicAddRequest request, Member currentMember)
    {
        var topic = new Topic
        {
            memberId = currentMember.Id,
            Title = request.Title,
            Category = request.Category,
            InitialMessage = request.InitialMessage
        };
        forumDb.Topics.Add(topic);
        await forumDb.SaveChangesAsync();
        return topic.Id;
    }

    public async Task<int> AddPost(PostAddRequest request, Member currentMember)
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
}