using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ALBackend.Entities.Members;
using ALBackend.Entities.Shared;

// ReSharper disable All

namespace ALMembers.Entities;

public class Member : Entity
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Title { get; set; }
    public DateTime JoinedDate { get; set; }
    public int ProfileImageId { get; set; }
    public Guid UserId { get; set; }
    public DateTime LastPayment { get; set; }
    public ICollection<ProfileImage> ProfileImages { get; set; } = [];
}