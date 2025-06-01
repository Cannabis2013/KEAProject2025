using ALBackend.DataTransferObject.Articles;
using ALBackend.Entities.Articles;
using ALBackend.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ALBackend.Services.Articles;

public class Articles(MariaDbContext dbContext) : IArticles
{
    public async Task<List<Article>> Paginated(int pageIndex, int pageSize, int memberId)
    {
        return await dbContext.Articles
            .Include(article => article.Image)
            .OrderByDescending(a => a.CreatedAt)
            .Skip(pageIndex)
            .Take(pageSize)
            .ToListAsync();
    }

    public List<Article> Many(int count)
    {
        return dbContext.Articles
            .AsEnumerable()
            .TakeLast(count)
            .OrderByDescending(article => article.CreatedAt)
            .ToList();
    }

    public Article One(int id)
    {
        return dbContext.Articles
            .First(article => article.Id == id);
    }
    
    private Article? FindArticleFromUser(int id, int memberId)
    {
        return dbContext
            .Articles
            .Include(article => article.Image)
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

        if (request.ImageBlob is not null && article.Image is not null)
            article.Image.Base64 = request.ImageBlob;
        else if (request.ImageBlob is not null)
            article.Image = new(request.ImageBlob);
        
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