using ALBackend.Entities.Shared;
using Microsoft.EntityFrameworkCore;

namespace ALBackend.Entities.Articles;

public class Article : Entity
{
    public string Headline { get; set; } = "";
    public string ShortContent { get; set; } = "";
    public string Content { get; set; } = "";
    public int MemberId { get; set; }
    
    public ArticleImage? Image { get; set; }
}