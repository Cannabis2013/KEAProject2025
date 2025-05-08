using ALBackend.DataTransferObject.Articles;
using ALBackend.Entities.Identity;

namespace ALBackend.Services.Articles;

public interface IArticlesUpdater
{
    public Task<bool> Create(ArticleCreateRequest request, UserAccount user);
}