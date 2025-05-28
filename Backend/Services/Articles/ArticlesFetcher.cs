using ALBackend.DataTransferObject.Articles;
using ALBackend.Persistence.Articles;
using ALBackend.Persistence.Members;

namespace ALBackend.Services.Articles;

public class ArticlesFetcher(ArticlesDb articlesDb, MembersDb membersDb) : IArticlesFetcher
{
    private string GetAuthor(int memberId)
    {
        var member = membersDb.Members
            .FirstOrDefault(member => member.Id == memberId);
        return $"{member?.FirstName} {member?.LastName}";
    }

    public List<ArticleFetchResponse> Paginated(int pageIndex, int pageSize, int memberId)
    {
        return articlesDb.Articles
            .OrderBy(a => a.Id)
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
        return articlesDb.Articles
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
        var article = articlesDb.Articles
            .First(article => article.Id == id);
        return new(article, GetAuthor(article.MemberId));
    }
}