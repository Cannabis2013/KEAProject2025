using ALBackend.DataTransferObject.Articles;
using ALBackend.Entities.Identity;

namespace ALBackend.Services.Articles;

public interface IArticlesUpdater
{
    public Task<bool> Create(ArticleUpdateRequest request, Guid userId);
    public Task<bool> Update(ArticleUpdateRequest request, Guid userId);
    public Task<bool> Remove(Guid id, Guid userId);
}