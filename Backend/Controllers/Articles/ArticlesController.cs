using ALBackend.DataTransferObject.Articles;
using ALBackend.Services.Articles;
using ALBackend.Services.Identity.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ALBackend.Controllers.Articles;

[ApiController, Route("/articles")]
public class ArticlesController(IUsersFetch userFetcher, IArticlesFetcher fetcher, IArticlesUpdater updater): ControllerBase
{
    [HttpGet,Authorize]
    public JsonResult Articles()
    {
        var articles = fetcher.Many();
        var user = userFetcher.User(User);
        articles.ForEach(article => article.IsOwner = article.userId.ToString() == user?.Id);
        return new(articles);
    }

    [HttpGet("count/{count:int}"),Authorize]
    public JsonResult Article(int count)
    {
        var articles = fetcher.Many(count);
        var user = userFetcher.User(User);
        articles.ForEach(article => article.IsOwner = article.userId.ToString() == user?.Id);
        return new(articles);
    }

    [HttpGet("user/{userId:guid}"),Authorize]
    public JsonResult Filtered(Guid userId)
    {
        var article = fetcher.OneFromUser(userId);
        return new(article);
    }

    [HttpGet("{id:guid}"),Authorize]
    public JsonResult One(Guid id)
    {
        var article = fetcher.One(id);
        return new(article);
    }

    [HttpPost, Authorize]
    public async Task<JsonResult> Create(ArticleUpdateRequest request)
    {   
        var user = userFetcher.User(User);
        if(user is null) return new(""){StatusCode = StatusCodes.Status404NotFound};
        var result = await updater.Create(request, Guid.Parse(user.Id));
        return new(result);
    }

    [HttpPatch, Authorize]
    public async Task<JsonResult> Update(ArticleUpdateRequest request)
    {
        var user = userFetcher.User(User);
        if(user is null) return new(""){StatusCode = StatusCodes.Status404NotFound};
        var result = await updater.Update(request, Guid.Parse(user.Id));
        return new(result);
    }

    [HttpDelete("{id:guid}"), Authorize]
    public async Task<JsonResult> Delete(Guid id)
    {
        var user = userFetcher.User(User);
        if(user is null) return new(""){StatusCode = StatusCodes.Status404NotFound};
        var result = await updater.Remove(id, Guid.Parse(user.Id));
        return new(result);
    }
}