using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ALBackend.Entities.Forum;

public class Topic
{
    [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public required string Title { get; init; } = "";
    public string Category { get; set; } = "";
    public required string InitialMessage { get; init; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public required int memberId { get; set; }
}