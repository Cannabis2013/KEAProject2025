using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ALBackend.Entities.Shared;

namespace ALBackend.Entities.Members;

public class ProfileImage : Entity
{
    public string? Blob { get; set; }
    [ForeignKey("id")]
    public required int memberId { get; set; }
}