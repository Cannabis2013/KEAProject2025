namespace ALBackend.DataTransferObject.Forum;

public class PostAddRequest
{
    public int TopicId { get; set; }
    public string Message { get; set; } = "";
}