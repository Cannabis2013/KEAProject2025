using ALBackend.Entities.Shared;

namespace ALBackend.Entities.Forum;

public class Post : Entity
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public required string Content { get; set; }
    public required int TopicId { get; set; }
    public required int memberId { get; set; }
}