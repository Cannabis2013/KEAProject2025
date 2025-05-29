using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using ALBackend.Entities.Shared;

namespace ALBackend.Entities.Articles;

public class ArticleImage() : Entity
{
    public ArticleImage(string base64) : this()
    {
        Base64 = base64;
    }
    
    public string Base64 { get; set; } = "";
    [ForeignKey("id")]
    public int ArticleId { get; set; }
}