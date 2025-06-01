using ALBackend.DataTransferObject.Articles;
using ALBackend.Entities.Articles;

namespace ALBackend.Services.Articles;

public interface IArticles
{
    public Task<List<Article>> Paginated(int pageIndex,int pageSize, int memberId = -1);
    public List<Article> Many(int memberId);
    public Article One(int id);
    public Task<bool> Create(ArticleUpdateRequest request, int memberId);
    public Task<bool> Update(ArticleUpdateRequest request, int memberId);
    public Task<bool> Remove(int id, int memberId);
}