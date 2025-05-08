namespace ALBackend.DataTransferObject.Articles;

public class ArticleCreateRequest
{
    public string Title { get; set; } = "";
    public string ShortContent { get; set; } = "";
    public string Content { get; set; } = "";
}