using System.Runtime.CompilerServices;
using ALBackend.Entities.Forum;
using ALMembers.Entities;

namespace ALBackend.DataTransferObject.Forum;

public class TopicFetchResponse(Topic topic,Member? creator,Member? current = null)
{
    public int Id { get; set; } = topic.Id;
    public string Title { get; set; } = topic.Title;
    public string Category { get; set; } = topic.Category;
    public bool IsOwner { get; set; } = current?.Id == topic.memberId;
    public int memberId { get; set; } = current?.Id ?? 0;
    public string author { get; set; } = $"{creator?.FirstName} {creator?.LastName}";
    public DateTime Created { get; set; } = topic.CreatedAt;
    public int PostsCount { get; set; } = 0;
    public string InitialMessage { get; set; } = "";
    public string LastPoster { get; set; } = "";
    public DateTime LastPostedOn { get; set; }
}