using ALBackend.DataTransferObject.Forum;
using ALBackend.Persistence.Members;
using ALBackend.Services.Forum;
using ALBackend.Services.Identity.Users;
using ALMembers.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ALBackend.Controllers.Forum;

[ApiController, Authorize]
public class ForumController(
    IUsersFetcher usersFetcher,
    MembersDb membersDb,
    ITopicFetcher topicFetcher,
    IPostFetcher postFetcher,
    IPostUpdater postUpdater,
    ITopicUpdater topicUpdater) : ControllerBase
{
    private Member? CurrentMember()
    {
        var user = usersFetcher.User(User);
        var userId = Guid.Parse(user?.Id ?? Guid.Empty.ToString());
        return membersDb.Members.FirstOrDefault(member => member.UserId == userId);
    }

    [HttpGet("topic/{lastIndex:int}/{count:int}")]
    public JsonResult GetTopics(int lastIndex, int count)
    {
        var member = CurrentMember();
        if (member is null) return new("") { StatusCode = StatusCodes.Status404NotFound };
        var topics = topicFetcher.TopicsWithoutPosts(lastIndex, count, member);
        return new(topics);
    }

    [HttpGet("post/{topicId:int}/{pageIndex:int}/{pageSize:int}")]
    public JsonResult GetPosts(int topicId, int pageIndex, int pageSize)
    {
        var member = CurrentMember();
        if (member is null) return new("") { StatusCode = StatusCodes.Status404NotFound };
        var posts = postFetcher.Posts(pageIndex, pageSize, topicId, member);
        return new(posts);
    }

    [HttpGet("topic/{id:int}")]
    public JsonResult GetTopic(int id)
    {
        var member = CurrentMember();
        if (member is null) return new("") { StatusCode = StatusCodes.Status404NotFound };
        var topic = topicFetcher.Topic(id, member);
        return new(topic);
    }

    [HttpPost("topic")]
    public async Task<JsonResult> AddTopic(TopicUpdateRequest request)
    {
        var member = CurrentMember();
        if (member is null) return new("") { StatusCode = StatusCodes.Status404NotFound };
        var topicId = await topicUpdater.AddTopic(request, member);
        return new(topicId);
    }

    [HttpPatch("topic")]
    public async Task<JsonResult> UpdateTopic(TopicUpdateRequest request)
    {
        var member = CurrentMember();
        if (member is null) return new("") { StatusCode = StatusCodes.Status404NotFound };
        var result = await topicUpdater.UpdateTopic(request, member);
        return new(result);
    }

    [HttpDelete("topic/{id:int}")]
    public async Task<JsonResult> RemoveTopic(int id)
    {
        var member = CurrentMember();
        if (member is null) return new("") { StatusCode = StatusCodes.Status404NotFound };
        var result = await topicUpdater.RemoveTopic(id,member);
        return new(result);
    }

    [HttpPost("post")]
    public async Task<JsonResult> AddPost(PostUpdateRequest request)
    {
        var member = CurrentMember();
        if (member is null) return new("") { StatusCode = StatusCodes.Status404NotFound };
        var postId = await postUpdater.AddPost(request, member);
        return new(postId);
    }

    [HttpGet("post/{postId:int}")]
    public JsonResult GetPost(int postId)
    {
        var post = postFetcher.Post(postId);
        if (post is null) return new("") { StatusCode = StatusCodes.Status404NotFound };
        return new(post);
    }

    [HttpPatch("post")]
    public async Task<JsonResult> UpdatePost(PostUpdateRequest request)
    {
        var member = CurrentMember();
        if (member is null) return new("") { StatusCode = StatusCodes.Status404NotFound };
        return new(await postUpdater.UpdatePost(request));
    }

    [HttpDelete("post/{postId:int}")]
    public async Task<JsonResult> DeletePost(int postId)
    {
        var member = CurrentMember();
        if (member is null) return new(false);
        var result = await postUpdater.RemovePost(postId);
        return new(result);
    }
}