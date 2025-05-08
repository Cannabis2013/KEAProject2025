using ALBackend.DataTransferObject.Articles;
using ALBackend.Entities.Articles;
using ALBackend.Entities.Identity;
using ALBackend.Persistence.Articles;
using ALBackend.Persistence.Members;

namespace ALBackend.Services.Articles;

public class ArticlesUpdater(ArticlesDb articles, MembersDb members) : IArticlesUpdater
{
    public async Task<bool> Create(ArticleCreateRequest request,UserAccount user)
    {
        var userId = Guid.Parse(user.Id);
        var member = members.Members.First(member => member.UserId == userId);
        var article = new Article()
        {
            Headline = request.Title,
            ShortContent = request.ShortContent,
            Content = request.Content,
            MemberId = member.Id
        };
        articles.Add(article);
        var result = await articles.SaveChangesAsync();
        return result > 0;
    }
}