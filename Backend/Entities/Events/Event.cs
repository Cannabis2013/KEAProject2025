using ALBackend.Entities.Shared;

namespace ALBackend.Entities.Events;

public class Event : Entity
{
    public DateTime EventDate { get; set; }
    public string EventName { get; set; } = "";
    public string EventDescription { get; set; } = "";
    public string EventLocation { get; set; } = "";
    public string EventManager { get; set; } = "";
}