using ALBackend.DataTransferObject.Articles;

namespace ALBackend.Services.Articles;

public interface IArticlesUpdater
{
    public Task<bool> Create(ArticleUpdateRequest request, int memberId);
    public Task<bool> Update(ArticleUpdateRequest request, int memberId);
    public Task<bool> Remove(int id, int memberId);
}