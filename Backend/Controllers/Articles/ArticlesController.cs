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
        var articles = fetcher.Fetch();
        return new(articles);
    }

    [HttpGet("count/{count:int}"),Authorize]
    public JsonResult Article(int count)
    {
        var articles = fetcher.Fetch(count);
        return new(articles);
    }

    [HttpGet("user/{userId:int}"),Authorize]
    public JsonResult Filtered(int userId) => fetcher.FromUser(userId);
    
    [HttpGet("{id:int}"),Authorize]
    public JsonResult One(int id) => fetcher.One(id);

    [HttpPost, Authorize]
    public async Task<JsonResult> Create(ArticleCreateRequest request)
    {   
        var user = userFetcher.User(User);
        if(user is null) return new(""){StatusCode = StatusCodes.Status404NotFound};
        var result = await updater.Create(request, user);
        return new(result);
    }
}