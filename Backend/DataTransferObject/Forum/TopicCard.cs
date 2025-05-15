using ALBackend.Entities.Forum;
using ALMembers.Entities;

namespace ALBackend.DataTransferObject.Forum;

public class TopicCard(Topic topic,Member member)
{
    public int Id { get; set; } = topic.Id;
    public string Title { get; set; } = topic.Title;
    public string Category { get; set; } = topic.Category;
    public required bool IsOwner { get; set; }
    public int memberId { get; set; } = member.Id;
    public string author { get; set; } = $"{member.FirstName} {member.LastName}";
    public DateTime Created { get; set; } = topic.CreatedAt;
    public string lastPoster { get; set; } = "";
    public required int PostsCount { get; set; } = 0;
    public required string InitialMessage { get; set; }
}