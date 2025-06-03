using ALBackend.Entities.Forum;
using ALMembers.Entities;

namespace ALBackend.DataTransferObject.Forum;

public class PostFetchRequest(Post post, Member? creator = null, Member? current = null)
{
    public int Id { get; set; } = post.Id;
    public string Message { get; set; } = post.Content;
    public int TopicId { get; set; } = post.TopicId;
    public DateTime Created { get; set; } = post.CreatedAt;
    public bool IsOwner { get; set; } = post.memberId == current?.Id;
    public int MemberId { get; set; } = post.memberId;
    public string Author { get; set; } = $"{creator?.FirstName} {creator?.LastName}";
}