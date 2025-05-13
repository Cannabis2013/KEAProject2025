using ALBackend.DataTransferObject.Articles;
using ALBackend.Entities.Articles;
using ALBackend.Persistence.Articles;

namespace ALBackend.Services.Articles;

public class ArticlesUpdater(ArticlesDb articlesDb) : IArticlesUpdater
{
    private Article? FindArticleFromUser(int id, Guid userId)
    {
        return articlesDb
            .Articles
            .FirstOrDefault(article => article.Id == id && article.UserId == userId);
    }
    
    public async Task<bool> Create(ArticleUpdateRequest request,Guid userId)
    {
        var article = new Article()
        {
            Headline = request.Headline,
            ShortContent = request.ShortContent,
            Content = request.Content,
            UserId = userId
        };
        articlesDb.Add(article);
        var result = await articlesDb.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> Update(ArticleUpdateRequest request, Guid userId)
    {
        var article = FindArticleFromUser(request.Id,userId);
        if(article is null) return false;
        article.Headline = request.Headline;
        article.ShortContent = request.ShortContent;
        article.Content = request.Content;
        articlesDb.Update(article);
        return await articlesDb.SaveChangesAsync() > 0;
    }

    public async Task<bool> Remove(int id, Guid userId)
    {
        var article = FindArticleFromUser(id,userId);
        if(article is null) return false;
        articlesDb.Remove(article);
        return await articlesDb.SaveChangesAsync() > 0;
    }
}