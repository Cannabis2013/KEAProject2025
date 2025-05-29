using System.ComponentModel.DataAnnotations.Schema;
using ALBackend.Entities.Shared;

namespace ALBackend.Entities.Events;

public class Event : Entity
{
    public DateTime EventStartDate { get; set; }
    public DateTime EventEndDate { get; set; }
    public string EventName { get; set; } = "";
    public string EventDescription { get; set; } = "";
    public string EventLocation { get; set; } = "";
    public string EventAuthor { get; set; } = "";
}