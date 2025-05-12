namespace ALBackend.DataTransferObject.Articles;

public class ArticleUpdateRequest
{
    public string Id { get; set; }
    public string Headline { get; set; } = "";
    public string ShortContent { get; set; } = "";
    public string Content { get; set; } = "";
}