using ALBackend.DataTransferObject.Articles;
using ALBackend.Entities.Articles;
using ALBackend.Persistence.Articles;
using ALBackend.Persistence.Members;
using Microsoft.AspNetCore.Mvc;

namespace ALBackend.Services.Articles;

public class ArticlesFetcher(ArticlesDb articlesDb, MembersDb members) : IArticlesFetcher
{
    private string GetAuthor(Article article)
    {
        var member = members.Members.Find(article.MemberId);
        return $"{member?.FirstName} {member?.LastName}";
    }

    public List<ArticleCard> Fetch()
    {
        return articlesDb.Articles
            .ToList()
            .OrderByDescending(article => article.CreatedAt)
            .Select(article =>
            {
                var author = GetAuthor(article);
                return new ArticleCard(article, author);
            })
            .ToList();
    }

    public List<ArticleCard> Fetch(int count)
    {
        return articlesDb.Articles
            .AsEnumerable()
            .TakeLast(count)
            .OrderByDescending(article => article.CreatedAt)
            .ToList()
            .Select(article =>
            {
                var author = GetAuthor(article);
                return new ArticleCard(article, author);
            })
            .ToList();
    }

    public JsonResult FromUser(int userId)
    {
        var reduced = articlesDb.Articles
            .Where(article => article.MemberId == userId)
            .AsEnumerable()
            .Select(GetAuthor)
            .ToList();
        return new(reduced);
    }

    public JsonResult One(int id)
    {
        var article = articlesDb.Articles.Find(id);
        return article is null
            ? new("Article not found")
            : new(GetAuthor(article));
    }
}