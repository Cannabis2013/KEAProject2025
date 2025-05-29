using ALBackend.Entities.Shared;

namespace ALBackend.Entities.Forum;

public class Topic : Entity
{
    public required string Title { get; set; } = "";
    public string Category { get; set; } = "";
    public required string InitialMessage { get; set; }
    public required int memberId { get; set; }
}