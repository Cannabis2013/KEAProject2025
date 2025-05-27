using ALBackend.DataTransferObject.Forum;
using ALBackend.Persistence.Members;
using ALBackend.Services.Forum;
using ALBackend.Services.Identity.Users;
using ALMembers.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ALBackend.Controllers.Forum;

[ApiController,Route("post"),Authorize]
public class PostController(
    IUsersFetcher usersFetcher,
    MembersDb membersDb,
    IPostFetcher postFetcher,
    IPostUpdater postUpdater) : ControllerBase
{
    private Member? CurrentMember()
    {
        var user = usersFetcher.User(User);
        var userId = Guid.Parse(user?.Id ?? Guid.Empty.ToString());
        return membersDb.Members.FirstOrDefault(member => member.UserId == userId);
    }

    [HttpGet("{topicId:int}/{pageIndex:int}/{pageSize:int}")]
    public JsonResult GetPosts(int topicId, int pageIndex, int pageSize)
    {
        var member = CurrentMember();
        if (member is null) return new("") { StatusCode = StatusCodes.Status404NotFound };
        var posts = postFetcher.AsBlocks(pageIndex, pageSize, topicId, member);
        return new(posts);
    }
    
    [HttpPost]
    public async Task<JsonResult> AddPost(PostUpdateRequest request)
    {
        var member = CurrentMember();
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
        var member = CurrentMember();
        if (member is null) return new("") { StatusCode = StatusCodes.Status404NotFound };
        return new(await postUpdater.UpdatePost(request));
    }

    [HttpDelete("{postId:int}")]
    public async Task<JsonResult> DeletePost(int postId)
    {
        var member = CurrentMember();
        if (member is null) return new(false);
        var result = await postUpdater.RemovePost(postId);
        return new(result);
    }

    [HttpGet("cards/{count:int}")]
    public JsonResult GetCards(int count)
    {
        var member = CurrentMember();
        if (member is null) return new("") { StatusCode = StatusCodes.Status404NotFound };
        var cards = postFetcher.AsCards(count);
        return new(cards);
    }
}