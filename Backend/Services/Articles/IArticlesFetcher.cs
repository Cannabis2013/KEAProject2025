using ALBackend.DataTransferObject.Articles;

namespace ALBackend.Services.Articles;

public interface IArticlesFetcher
{
    public List<ArticleCard> Paginated(int lastId,int count, Guid userId);
    public List<ArticleCard> Many(Guid userId);
    public List<ArticleCard> Many(int count);
    public ArticleCard One(int id);
}