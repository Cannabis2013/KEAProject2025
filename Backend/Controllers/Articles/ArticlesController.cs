using ALBackend.DataTransferObject.Articles;
using ALBackend.Services.Articles;
using ALBackend.Services.Members;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ALBackend.Controllers.Articles;

[ApiController, Route("/articles")]
public class ArticlesController(
    IMembers members,
    IArticlesFetcher fetcher,
    IArticlesUpdater updater) : ControllerBase
{
    [HttpGet("{lastIndex:int}/{count:int}")]
    public JsonResult Articles(int lastIndex, int count)
    {
        var memberId = members.One(User)?.Id ?? -1;
        var articles = fetcher.Paginated(lastIndex, count, memberId);
        return new(articles);
    }

    [HttpGet("count/{count:int}")]
    public JsonResult Article(int count)
    {
        var articles = fetcher.Many(count);
        return new(articles);
    }

    [HttpGet("{id:int}"), Authorize]
    public JsonResult One(int id)
    {
        var article = fetcher.One(id);
        return new(article);
    }

    [HttpPost, Authorize]
    public async Task<JsonResult> Create(ArticleUpdateRequest request)
    {
        var member = members.One(User);
        if (member is null) return new("Member not found") { StatusCode = StatusCodes.Status404NotFound };
        var result = await updater.Create(request, member.Id);
        return new(result);
    }

    [HttpPatch, Authorize]
    public async Task<JsonResult> Update(ArticleUpdateRequest request)
    {
        var member = members.One(User);
        if (member is null) return new("Member not found") { StatusCode = StatusCodes.Status404NotFound };
        var result = await updater.Update(request, member.Id);
        return new(result);
    }

    [HttpDelete("{id:int}"), Authorize]
    public async Task<JsonResult> Delete(int id)
    {
        var member = members.One(User);
        if (member is null) return new("Member not found") { StatusCode = StatusCodes.Status404NotFound };
        var result = await updater.Remove(id, member.Id);
        return new(result);
    }
}