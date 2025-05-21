using ALBackend.Entities.Forum;

namespace ALBackend.DataTransferObject.Forum;

public class TopicUpdateRequest
{
    public int TopicId { get; set; }
    public string Title { get; set; } = "";
    public string Category { get; set; } = "";
    public string InitialMessage { get; set; } = "";

    public Topic ToEntity(int memberId)
    {
        return new()
        {
            Title = Title,
            Category = Category,
            InitialMessage = InitialMessage,
            memberId = memberId
        };
    }

    public Topic ToUpdateEntity(Topic topic)
    {
        topic.Title = Title;
        topic.Category = Category;
        topic.InitialMessage = InitialMessage;
        return topic;
    }
}