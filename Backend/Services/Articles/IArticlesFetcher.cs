using ALBackend.DataTransferObject.Articles;

namespace ALBackend.Services.Articles;

public interface IArticlesFetcher
{
    public List<ArticleCard> Many();
    public List<ArticleCard> Many(int count);
    public ArticleCard OneFromUser(Guid userId);
    public ArticleCard One(Guid id);
}