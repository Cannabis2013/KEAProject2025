using ALBackend.Entities.Articles;

namespace ALBackend.DataTransferObject.Articles;

public class ArticleBlock(Article article, string author)
{
    public Guid Id { get; set; } = article.Id;
    public string Headline { get; set; } = article.Headline;
    public string Content { get; set; } = article.Content;
    public string ImageUrl { get; set; } = article.ImageUrl;
    public DateTime Created { get; set; } = article.CreatedAt;
    public string Author { get; set; } = author;
}