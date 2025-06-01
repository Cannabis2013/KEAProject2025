using ALBackend.DataTransferObject.Articles;
using ALBackend.Services.Articles;
using ALBackend.Services.Members;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ALBackend.Controllers.Articles;

[ApiController, Route("/articles")]
public class ArticlesController(
    IMembers members,
    IArticles articles) : ControllerBase
{
    [HttpGet("{lastId:int}/{pageSize:int}")]
    public async Task<JsonResult> PaginatedArticles(int lastId, int pageSize)
    {
        var memberId = members.One(User)?.Id ?? -1;
        var result = await articles.Paginated(lastId, pageSize, memberId);
        var response = result
            .Select(article =>
            {
                var author = members.One(memberId);
                return new ArticleFetchResponse(article, author, memberId);
            })
            .ToList();
        return new(response);
    }

    [HttpGet("count/{count:int}")]
    public JsonResult Article(int count)
    {
        var result = articles.Many(count)
            .Select(article =>
            {
                var author = members.One(article.MemberId);
                return new ArticleFetchResponse(article, author);
            })
            .ToList();
        return new(result);
    }

    [HttpGet("{id:int}"), Authorize]
    public JsonResult One(int id)
    {
        var article = articles.One(id);
        var author = members.One(article.MemberId);
        var response = new ArticleFetchResponse(article, author);
        return new(response);
    }

    [HttpPost, Authorize]
    public async Task<JsonResult> Create(ArticleUpdateRequest request)
    {
        var member = members.One(User);
        if (member is null) return new("Member not found") { StatusCode = StatusCodes.Status404NotFound };
        var result = await articles.Create(request, member.Id);
        return new(result);
    }

    [HttpPatch, Authorize]
    public async Task<JsonResult> Update(ArticleUpdateRequest request)
    {
        var member = members.One(User);
        if (member is null) return new("Member not found") { StatusCode = StatusCodes.Status404NotFound };
        var result = await articles.Update(request, member.Id);
        return new(result);
    }

    [HttpDelete("{id:int}"), Authorize]
    public async Task<JsonResult> Delete(int id)
    {
        var member = members.One(User);
        if (member is null) return new("Member not found") { StatusCode = StatusCodes.Status404NotFound };
        var result = await articles.Remove(id, member.Id);
        return new(result);
    }
}