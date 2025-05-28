using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ALBackend.Entities.Shared;

namespace ALBackend.Entities.Members;

public class ImageLink : Entity
{
    public required string Url { get; set; }
    [ForeignKey("id")]
    public int memberId { get; set; }
}