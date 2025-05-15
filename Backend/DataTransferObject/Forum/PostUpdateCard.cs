using ALBackend.Entities.Forum;

namespace ALBackend.DataTransferObject.Forum;

public class PostUpdateCard(Post post)
{
    public int Id { get; set; } = post.Id;
    public string Message { get; set; } = post.Content;
    public int TopicId { get; set; } = post.TopicId;
    public DateTime Created { get; set; } = post.CreatedAt;
    public int MemberId { get; set; } = post.memberId;
}