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
    [HttpGet("{pageIndex:int}/{pageSize:int}")]
    public async Task<JsonResult> Paginated(int pageIndex, int pageSize)
    {
        var memberId = members.One(User)?.Id ?? -1;
        var response = (await articles.ManyAsync(pageIndex, pageSize, memberId))
            .Select(article =>
            {
                var author = members.One(memberId);
                return new ArticleFetchResponse(article, author, memberId);
            })
            .ToList();
        return new(response);
    }

    [HttpGet("count/{count:int}")]
    public async Task<JsonResult> Articles(int count)
    {
        var result = (await articles.ManyAsync(count))
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
        if (article == null) return new("") { StatusCode = StatusCodes.Status404NotFound };
        var author = members.One(article.MemberId);
        var response = new ArticleFetchResponse(article, author);
        return new(response);
    }

    [HttpPost, Authorize]
    public async Task<JsonResult> Create(ArticleUpdateRequest request)
    {
        var member = members.One(User);
        if (member is null) return new("Member not found") { StatusCode = StatusCodes.Status404NotFound };
        var result = await articles.AddAsync(request, member.Id);
        return new(result);
    }

    [HttpPatch, Authorize]
    public async Task<JsonResult> Update(ArticleUpdateRequest request)
    {
        var member = members.One(User);
        if (member is null) return new("Member not found") { StatusCode = StatusCodes.Status404NotFound };
        var result = await articles.UpdateAsync(request, member.Id);
        return new(result);
    }

    [HttpDelete("{id:int}"), Authorize]
    public async Task<JsonResult> Delete(int id)
    {
        var member = members.One(User);
        if (member is null) return new("Member not found") { StatusCode = StatusCodes.Status404NotFound };
        var result = await articles.RemoveAsync(id, member.Id);
        return new(result);
    }
}