using ALBackend.DataTransferObject.Forum;
using ALBackend.Services.Forum;
using ALBackend.Services.Members;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ALBackend.Controllers.Forum;

[ApiController,Route("post"),Authorize]
public class PostController(
    IMembers members,
    IPosts posts) : ControllerBase
{
    [HttpGet("{topicId:int}/{pageIndex:int}/{pageSize:int}")]
    public async Task<JsonResult> GetPosts(int topicId, int pageIndex, int pageSize)
    {
        var currentMember = members.One(User);
        if (currentMember is null) return new("") { StatusCode = StatusCodes.Status404NotFound };
        var result = await posts.ManyAsync(pageIndex, pageSize, topicId, currentMember);
        var response = result.Select(post =>
            {
                var creator = members.One(post.memberId);
                return new PostFetchRequest(post, creator, currentMember);
            })
            .ToList();
        return new(response);
    }
    
    [HttpPost]
    public async Task<JsonResult> AddPost(PostUpdateRequest request)
    {
        var member = members.One(User);
        if (member is null) return new("") { StatusCode = StatusCodes.Status404NotFound };
        var postId = await posts.AddAsync(request, member);
        return new(postId);
    }

    [HttpGet("{id:int}")]
    public async Task<JsonResult> GetPost(int id)
    {
        var currentMember = members.One(User);
        var post = await posts.OneAsync(id);
        if (post is null) return new("") { StatusCode = StatusCodes.Status404NotFound };
        var creator = members.One(post.memberId);
        var response = new PostFetchRequest(post, creator,currentMember);
        return new(response);
    }

    [HttpPatch]
    public async Task<JsonResult> UpdatePost(PostUpdateRequest request)
    {
        var member = members.One(User);
        if (member is null) return new("") { StatusCode = StatusCodes.Status404NotFound };
        return new(await posts.UpdateAsync(request));
    }

    [HttpDelete("{id:int}")]
    public async Task<JsonResult> DeletePost(int id)
    {
        var member = members.One(User);
        if (member is null) return new(false);
        var result = await posts.RemoveAsync(id);
        return new(result);
    }
}