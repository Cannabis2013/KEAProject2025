using ALBackend.Entities.Articles;
using ALMembers.Entities;

namespace ALBackend.DataTransferObject.Articles;

public class ArticleFetchResponse(Article article, Member? author, int memberId = -1)
{
    public int Id { get; set; } = article.Id;
    public string Headline { get; set; } = article.Headline;
    public string ShortContent { get; set; } = article.ShortContent;
    public string Content { get; set; } = article.Content;
    public string ImageBase64 { get; set; } = article.Image?.Base64 ?? "";
    public DateTime Created { get; set; } = article.CreatedAt;
    public string Author { get; set; } = $"{author?.FirstName} {author?.LastName}";
    public int memberId { get; set; } = article.MemberId;
    public bool IsOwner { get; set; } = memberId != -1 && memberId == article.MemberId;
}