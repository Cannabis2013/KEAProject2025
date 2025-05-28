using ALBackend.DataTransferObject.Articles;

namespace ALBackend.Services.Articles;

public interface IArticlesFetcher
{
    public List<ArticleFetchResponse> Paginated(int pageIndex,int pageSize, int memberId = -1);
    public List<ArticleFetchResponse> Many(int memberId);
    public ArticleFetchResponse One(int id);
}