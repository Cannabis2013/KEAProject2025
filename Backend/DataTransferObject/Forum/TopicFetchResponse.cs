using ALBackend.Entities.Forum;
using ALMembers.Entities;

namespace ALBackend.DataTransferObject.Forum;

public class TopicFetchResponse(Topic topic,Member? member = null)
{
    public int Id { get; set; } = topic.Id;
    public string Title { get; set; } = topic.Title;
    public string Category { get; set; } = topic.Category;
    public bool IsOwner { get; set; }
    public int memberId { get; set; } = member?.Id ?? 0;
    public string author { get; set; } = $"{member?.FirstName} {member?.LastName}";
    public DateTime Created { get; set; } = topic.CreatedAt;
    public string lastPoster { get; set; } = "";
    public int PostsCount { get; set; } = 0;
    public string InitialMessage { get; set; } = "";
    public DateTime LastPost { get; set; }
}