using System.ComponentModel.DataAnnotations.Schema;
using ALBackend.Entities.Shared;

namespace ALBackend.Entities.ImageGallary;

[Table("ImageGallary")]
public class Image : Entity
{
    public string Base64 { get; set; } = "";
    public string? Url { get; set; }
    public int? Width { get; set; }
    public int? Height { get; set; }
    public string? Description { get; set; }
}