using ALBackend.DataTransferObject.Articles;
using ALBackend.Persistence.Members;
using ALBackend.Services.Articles;
using ALBackend.Services.Identity.Users;
using ALBackend.Services.Members;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ALBackend.Controllers.Articles;

[ApiController, Route("/articles")]
public class ArticlesController(
    IUsersFetcher userFetcher,
    IArticlesFetcher fetcher,
    IArticlesUpdater updater) : ControllerBase
{
    [HttpGet, Authorize]
    public JsonResult Articles()
    {
        var user = userFetcher.User(User);
        var articles = fetcher.Many(Guid.Parse(user?.Id ?? ""));
        return new(articles);
    }

    [HttpGet("{lastIndex:int}/{count:int}"), Authorize]
    public JsonResult Articles(int lastIndex, int count)
    {
        var user = userFetcher.User(User);
        var articles = fetcher.Paginated(lastIndex, count, Guid.Parse(user?.Id ?? ""));
        return new(articles);
    }

    [HttpGet("count/{count:int}"), Authorize]
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
        var user = userFetcher.User(User);
        if (user is null) return new("") { StatusCode = StatusCodes.Status404NotFound };
        var result = await updater.Create(request, Guid.Parse(user.Id));
        return new(result);
    }

    [HttpPatch, Authorize]
    public async Task<JsonResult> Update(ArticleUpdateRequest request)
    {
        var user = userFetcher.User(User);
        if (user is null) return new("") { StatusCode = StatusCodes.Status404NotFound };
        var result = await updater.Update(request, Guid.Parse(user.Id));
        return new(result);
    }

    [HttpDelete("{id:int}"), Authorize]
    public async Task<JsonResult> Delete(int id)
    {
        var user = userFetcher.User(User);
        if (user is null) return new("") { StatusCode = StatusCodes.Status404NotFound };
        var result = await updater.Remove(id, Guid.Parse(user.Id));
        return new(result);
    }
}