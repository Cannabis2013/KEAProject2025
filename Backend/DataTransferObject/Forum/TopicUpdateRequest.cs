using ALBackend.Entities.Forum;

namespace ALBackend.DataTransferObject.Forum;

public class TopicUpdateRequest
{
    public string Title { get; set; } = "";
    public string Category { get; set; } = "";
    public string InitialMessage { get; set; } = "";
}