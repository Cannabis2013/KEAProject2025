using ALBackend.DataTransferObject.Forum;
using ALBackend.Services.Forum;
using ALBackend.Services.Members;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ALBackend.Controllers.Forum;

[ApiController,Route("post"),Authorize]
public class PostController(
    IMembers members,
    IPostFetcher postFetcher,
    IPostUpdater postUpdater) : ControllerBase
{
    [HttpGet("{topicId:int}/{pageIndex:int}/{pageSize:int}")]
    public JsonResult GetPosts(int topicId, int pageIndex, int pageSize)
    {
        var member = members.One(User);
        if (member is null) return new("") { StatusCode = StatusCodes.Status404NotFound };
        var posts = postFetcher.AsBlocks(pageIndex, pageSize, topicId, member);
        return new(posts);
    }
    
    [HttpPost]
    public async Task<JsonResult> AddPost(PostUpdateRequest request)
    {
        var member = members.One(User);
        if (member is null) return new("") { StatusCode = StatusCodes.Status404NotFound };
        var postId = await postUpdater.AddPost(request, member);
        return new(postId);
    }

    [HttpGet("{postId:int}")]
    public JsonResult GetPost(int postId)
    {
        var post = postFetcher.Post(postId);
        if (post is null) return new("") { StatusCode = StatusCodes.Status404NotFound };
        return new(post);
    }

    [HttpPatch]
    public async Task<JsonResult> UpdatePost(PostUpdateRequest request)
    {
        var member = members.One(User);
        if (member is null) return new("") { StatusCode = StatusCodes.Status404NotFound };
        return new(await postUpdater.UpdatePost(request));
    }

    [HttpDelete("{postId:int}")]
    public async Task<JsonResult> DeletePost(int postId)
    {
        var member = members.One(User);
        if (member is null) return new(false);
        var result = await postUpdater.RemovePost(postId);
        return new(result);
    }

    [HttpGet("cards/{count:int}")]
    public JsonResult GetCards(int count)
    {
        var member = members.One(User);
        if (member is null) return new("") { StatusCode = StatusCodes.Status404NotFound };
        var cards = postFetcher.AsCards(count);
        return new(cards);
    }
}