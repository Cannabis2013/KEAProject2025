using ALBackend.Entities.Shared;

namespace ALBackend.Entities.Articles;

public class Article : Entity
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public required string Headline { get; set; }
    public required string ShortContent  { get; set; }
    public required string Content { get; set; }
    public string ImageUrl { get; set; } = "";
    public int MemberId { get; set; }
}