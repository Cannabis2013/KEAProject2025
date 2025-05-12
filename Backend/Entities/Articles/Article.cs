using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ALBackend.Entities.Articles;

public class Article
{
    [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public required string Headline { get; set; }
    public required string ShortContent  { get; set; }
    public required string Content { get; set; }
    public string ImageUrl { get; set; } = "";
    public required Guid UserId { get; set; }
}