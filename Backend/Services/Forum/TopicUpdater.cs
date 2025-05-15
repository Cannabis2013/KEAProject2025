using ALBackend.DataTransferObject.Forum;
using ALBackend.Entities.Forum;
using ALBackend.Persistence.Forum;
using ALMembers.Entities;

namespace ALBackend.Services.Forum;

public class TopicUpdater(ForumDb forumDb) : ITopicUpdater
{
    public async Task<int> AddTopic(TopicUpdateRequest request, Member currentMember)
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

    public Task<bool> UpdateTopic(TopicUpdateRequest request)
    {
        throw new NotImplementedException();
    }

    
}