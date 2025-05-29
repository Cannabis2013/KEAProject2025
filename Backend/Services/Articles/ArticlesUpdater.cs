using ALBackend.DataTransferObject.Articles;
using ALBackend.Entities.Articles;
using ALBackend.Persistence;

namespace ALBackend.Services.Articles;

public class ArticlesUpdater(MariaDbContext dbContext) : IArticlesUpdater
{
    private Article? FindArticleFromUser(int id, int memberId)
    {
        return dbContext
            .Articles
            .FirstOrDefault(article => article.Id == id && article.MemberId == memberId);
    }

    public async Task<bool> Create(ArticleUpdateRequest request, int memberId)
    {
        var article = new Article
        {
            Headline = request.Headline,
            ShortContent = request.ShortContent,
            Content = request.Content,
            MemberId = memberId,
            Image = request.ImageBlob is not null ? new(request.ImageBlob) : null
        };

        dbContext.Articles.Add(article);
        return await dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> Update(ArticleUpdateRequest request, int memberId)
    {
        var article = FindArticleFromUser(request.Id, memberId);
        if (article is null) return false;
        article.Headline = request.Headline;
        article.ShortContent = request.ShortContent;
        article.Content = request.Content;
        dbContext.Update(article);
        return await dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> Remove(int id, int memberId)
    {
        var article = FindArticleFromUser(id, memberId);
        if (article is null) return false;
        dbContext.Remove(article);
        return await dbContext.SaveChangesAsync() > 0;
    }
}