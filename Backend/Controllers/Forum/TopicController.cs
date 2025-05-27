using ALBackend.DataTransferObject.Forum;
using ALBackend.Persistence.Members;
using ALBackend.Services.Forum;
using ALBackend.Services.Identity.Users;
using ALMembers.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ALBackend.Controllers.Forum;

[ApiController, Route("topic"), Authorize]
public class TopicController(
    IUsersFetcher usersFetcher,
    MembersDb membersDb,
    ITopicFetcher topicFetcher,
    ITopicUpdater topicUpdater) : ControllerBase
{
    private Member? CurrentMember()
    {
        var user = usersFetcher.User(User);
        var userId = Guid.Parse(user?.Id ?? Guid.Empty.ToString());
        return membersDb.Members.FirstOrDefault(member => member.UserId == userId);
    }

    [HttpGet("{id:int}")]
    public JsonResult GetTopic(int id)
    {
        var member = CurrentMember();
        if (member is null) return new("") { StatusCode = StatusCodes.Status404NotFound };
        var topic = topicFetcher.Topic(id, member);
        return new(topic);
    }
    
    [HttpGet("{lastIndex:int}/{count:int}")]
    public JsonResult GetTopics(int lastIndex, int count)
    {
        var member = CurrentMember();
        if (member is null) return new("") { StatusCode = StatusCodes.Status404NotFound };
        var topics = topicFetcher.TopicsWithoutPosts(lastIndex, count, member);
        return new(topics);
    }

    [HttpGet("active/{count:int}")]
    public JsonResult GetActiveTopics(int count)
    {
        var active = topicFetcher.RecentlyActive(count);
        return new(active);
    }

    [HttpPost]
    public async Task<JsonResult> AddTopic(TopicUpdateRequest request)
    {
        var member = CurrentMember();
        if (member is null) return new("") { StatusCode = StatusCodes.Status404NotFound };
        var topicId = await topicUpdater.AddTopic(request, member);
        return new(topicId);
    }

    [HttpPatch]
    public async Task<JsonResult> UpdateTopic(TopicUpdateRequest request)
    {
        var member = CurrentMember();
        if (member is null) return new("") { StatusCode = StatusCodes.Status404NotFound };
        var result = await topicUpdater.UpdateTopic(request, member);
        return new(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<JsonResult> RemoveTopic(int id)
    {
        var member = CurrentMember();
        if (member is null) return new("") { StatusCode = StatusCodes.Status404NotFound };
        var result = await topicUpdater.RemoveTopic(id,member);
        return new(result);
    }
}