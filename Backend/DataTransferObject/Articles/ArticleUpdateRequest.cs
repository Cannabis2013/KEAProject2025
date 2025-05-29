namespace ALBackend.DataTransferObject.Articles;

public class ArticleUpdateRequest
{
    public int Id { get; set; }
    public string Headline { get; set; } = "";
    public string ShortContent { get; set; } = "";
    public string Content { get; set; } = "";
    public string? ImageBlob { get; set; }
}