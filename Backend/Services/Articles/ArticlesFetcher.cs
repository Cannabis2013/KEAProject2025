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

    public List<ArticleCard> Paginated(int lastId, int count, Guid userId)
    {
        return articlesDb.Articles
            .Where((a) => a.Id > lastId)
            .OrderBy(a => a.Id)
            .Take(count)
            .ToList()
            .Select(article =>
            {
                var author = GetAuthor(article.UserId);
                return new ArticleCard(article, author)
                {
                    IsOwner = article.UserId == userId
                    
                };
            })
            .ToList();
        throw new NotImplementedException();
    }

    public List<ArticleCard> Many(Guid userId)
    {
        return articlesDb.Articles
            .OrderByDescending(article => article.CreatedAt)
            .AsEnumerable()
            .Select(article =>
            {
                var author = GetAuthor(article.UserId);
                return new ArticleCard(article, author)
                {
                    IsOwner = article.UserId == userId
                    
                };
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

    public ArticleCard One(int id)
    {
        var article = articlesDb.Articles
            .First(article => article.Id == id);
        return new(article, GetAuthor(article.UserId));
    }
}