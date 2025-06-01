using ALBackend.Entities.Shared;
using Microsoft.EntityFrameworkCore;

namespace ALBackend.Entities.Articles;

public class Article : Entity
{
    public required string Headline { get; set; }
    public required string ShortContent  { get; set; }
    public required string Content { get; set; }
    public int MemberId { get; set; }
    
    public ArticleImage? Image { get; set; }
}