using ALBackend.DataTransferObject.Articles;
using ALBackend.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ALBackend.Services.Articles;

public class ArticlesFetcher(MariaDbContext dbContext) : IArticlesFetcher
{
    private string GetAuthor(int memberId)
    {
        var member = dbContext.Members
            .FirstOrDefault(member => member.Id == memberId);
        return $"{member?.FirstName} {member?.LastName}";
    }

    public List<ArticleFetchResponse> Paginated(int pageIndex, int pageSize, int memberId)
    {
        return dbContext.Articles
            .Include(article => article.Image)
            .OrderByDescending(a => a.CreatedAt)
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToList()
            .Select(article =>
            {
                var author = GetAuthor(article.MemberId);
                return new ArticleFetchResponse(article, author)
                {
                    IsOwner = memberId != -1 && article.MemberId == memberId
                };
            })
            .ToList();
    }

    public List<ArticleFetchResponse> Many(int count)
    {
        return dbContext.Articles
            .AsEnumerable()
            .TakeLast(count)
            .OrderByDescending(article => article.CreatedAt)
            .ToList()
            .Select(article =>
            {
                var author = GetAuthor(article.MemberId);
                return new ArticleFetchResponse(article, author);
            })
            .ToList();
    }

    public ArticleFetchResponse One(int id)
    {
        var article = dbContext.Articles
            .First(article => article.Id == id);
        return new(article, GetAuthor(article.MemberId));
    }
}