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
    IForumFetcher forumFetcher,
    IForumUpdater forumUpdater) : ControllerBase
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
        var topics = forumFetcher.ManyWithoutPosts(lastIndex, count, member);
        return new(topics);
    }

    [HttpGet("post/{topicId:int}/{pageIndex:int}/{pageSize:int}")]
    public JsonResult GetPosts(int topicId, int pageIndex, int pageSize)
    {
        var member = CurrentMember();
        if (member is null) return new("") { StatusCode = StatusCodes.Status404NotFound };
        var posts = forumFetcher.Posts(pageIndex, pageSize, topicId, member);
        return new(posts);
    }

    [HttpGet("topic/{id:int}")]
    public JsonResult GetTopic(int id)
    {
        var member = CurrentMember();
        if (member is null) return new("") { StatusCode = StatusCodes.Status404NotFound };
        var topic = forumFetcher.One(id, member);
        return new(topic);
    }

    [HttpPost("topic")]
    public async Task<JsonResult> AddTopic(TopicAddRequest request)
    {
        var member = CurrentMember();
        if (member is null) return new("") { StatusCode = StatusCodes.Status404NotFound };
        var topicId = await forumUpdater.AddTopic(request, member);
        return new(topicId);
    }

    [HttpPost("post")]
    public async Task<JsonResult> AddPost(PostAddRequest request)
    {
        var member = CurrentMember();
        if (member is null) return new("") { StatusCode = StatusCodes.Status404NotFound };
        var postId = await forumUpdater.AddPost(request, member);
        return new(postId);
    }
}