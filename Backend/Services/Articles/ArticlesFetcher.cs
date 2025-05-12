using ALBackend.DataTransferObject.Articles;
using ALBackend.Persistence.Articles;
using ALBackend.Persistence.Members;

namespace ALBackend.Services.Articles;

public class ArticlesFetcher(ArticlesDb articlesDb, MembersDb membersDb) : IArticlesFetcher
{
    private string GetAuthor(Guid userId)
    {
        var member = membersDb.Members
            .FirstOrDefault(member => member.UserId == userId);
        return $"{member?.FirstName} {member?.LastName}";
    }

    public List<ArticleCard> Many()
    {
        return articlesDb.Articles
            .ToList()
            .OrderByDescending(article => article.CreatedAt)
            .Select(article =>
            {
                var author = GetAuthor(article.UserId);
                return new ArticleCard(article, author);
            })
            .ToList();
    }

    public List<ArticleCard> Many(int count)
    {
        return articlesDb.Articles
            .AsEnumerable()
            .TakeLast(count)
            .OrderByDescending(article => article.CreatedAt)
            .ToList()
            .Select(article =>
            {
                var author = GetAuthor(article.UserId);
                return new ArticleCard(article, author);
            })
            .ToList();
    }

    public ArticleCard OneFromUser(Guid userId)
    {
        var article = articlesDb.Articles
            .First(article => article.UserId == userId);
        return new(article, GetAuthor(article.UserId));
    }

    public ArticleCard One(Guid id)
    {
        var article = articlesDb.Articles
            .First(article => article.Id == id);
        return new(article, GetAuthor(article.UserId));
    }
}