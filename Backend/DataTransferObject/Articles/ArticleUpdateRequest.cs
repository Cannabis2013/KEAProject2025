using ALBackend.Entities.Articles;

namespace ALBackend.DataTransferObject.Articles;

public class ArticleUpdateRequest
{
    public int Id { get; set; }
    public string Headline { get; set; } = "";
    public string ShortContent { get; set; } = "";
    public string Content { get; set; } = "";
    public string? ImageBlob { get; set; }

    public Article ToEntity()
    {
        return new()
        {
            Headline = Headline,
            ShortContent = ShortContent,
            Content = Content,
            Image = ImageBlob is not null ? new(ImageBlob) : null
        };
    }

    public Article ToEntity(Article article)
    {
        article.Headline = Headline;
        article.ShortContent = ShortContent;
        article.Content = Content;
        return article;
    }
}