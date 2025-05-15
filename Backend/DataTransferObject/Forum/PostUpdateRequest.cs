namespace ALBackend.DataTransferObject.Forum;

public class PostUpdateRequest
{
    public int Id { get; set; }
    public int TopicId { get; set; }
    public string Message { get; set; } = "";
}