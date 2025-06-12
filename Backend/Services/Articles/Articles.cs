using ALBackend.DataTransferObject.Articles;
using ALBackend.Entities.Articles;
using ALBackend.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ALBackend.Services.Articles;

public class Articles(MariaDbContext dbContext) : IArticles
{
    public async Task<List<Article>> ManyAsync(int pageIndex, int pageSize, int memberId)
    {
        return await dbContext.Articles
            .Include(article => article.Image)
            .OrderByDescending(a => a.CreatedAt)
            .Skip(pageIndex)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<List<Article>> ManyAsync(int count)
    {
        return (await dbContext.Articles.ToListAsync())
            .TakeLast(count)
            .OrderByDescending(article => article.CreatedAt)
            .ToList();
    }

    public Article? One(int id) =>
        dbContext.Articles.FirstOrDefault(article => article.Id == id);

    private Article? FindArticleFromUser(int id, int memberId)
    {
        return dbContext
            .Articles
            .Include(article => article.Image)
            .FirstOrDefault(article => article.Id == id && article.MemberId == memberId);
    }

    public async Task<bool> AddAsync(ArticleUpdateRequest request, int memberId)
    {
        var article = request.ToEntity();
        article.MemberId = memberId;
        dbContext.Articles.Add(article);
        return await dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(ArticleUpdateRequest request, int memberId)
    {
        var article = FindArticleFromUser(request.Id, memberId);
        if (article is null) return false;
        
        var updated = request.ToEntity(article);
        
        if (request.ImageBlob is not null && updated.Image is not null)
            updated.Image.Base64 = request.ImageBlob;
        else if (request.ImageBlob is not null)
            updated.Image = new(request.ImageBlob);
        
        dbContext.Update(updated);
        return await dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> RemoveAsync(int id, int memberId)
    {
        var article = FindArticleFromUser(id, memberId);
        if (article is null) return false;
        dbContext.Remove(article);
        return await dbContext.SaveChangesAsync() > 0;
    }
}