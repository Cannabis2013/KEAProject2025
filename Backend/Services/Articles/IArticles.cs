using ALBackend.DataTransferObject.Articles;
using ALBackend.Entities.Articles;

namespace ALBackend.Services.Articles;

public interface IArticles
{
    public Task<List<Article>> ManyAsync(int pageIndex,int pageSize, int memberId = -1);
    public Task<List<Article>> ManyAsync(int memberId);
    public Article? One(int id);
    public Task<bool> AddAsync(ArticleUpdateRequest request, int memberId);
    public Task<bool> UpdateAsync(ArticleUpdateRequest request, int memberId);
    public Task<bool> RemoveAsync(int id, int memberId);
}