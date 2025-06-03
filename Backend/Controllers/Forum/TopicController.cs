using ALBackend.DataTransferObject.Forum;
using ALBackend.Services.Forum;
using ALBackend.Services.Members;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ALBackend.Controllers.Forum;

[ApiController, Route("topic"), Authorize]
public class TopicController(
    IMembers members,
    ITopics topics,
    ITopicResponse responses) : ControllerBase
{
    [HttpGet("{id:int}")]
    public async Task<JsonResult> GetTopic(int id)
    {
        var currentMember = members.One(User);
        if (currentMember is null) return new("") { StatusCode = StatusCodes.Status404NotFound };
        var topic = await topics.OneAsync(id);
        if (topic is null) return new("") { StatusCode = StatusCodes.Status404NotFound };
        var topicCreator = members.One(topic.memberId);
        return new(new TopicFetchResponse(topic,topicCreator,currentMember));
    }
    
    [HttpGet("{lastIndex:int}/{count:int}")]
    public async Task<JsonResult> GetTopics(int lastIndex, int count)
    {
        var member = members.One(User);
        if (member is null) return new("") { StatusCode = StatusCodes.Status404NotFound };
        var result = await topics.ManyAsync(lastIndex, count, member);
        var response = result
            .Select(topic => responses.ResponseWithPosterInfo(topic,member))
            .ToList();
        return new(response);
    }

    [HttpGet("active/{count:int}")]
    public async Task<JsonResult> GetActiveTopics(int count)
    {
        var active = await topics.RecentlyActiveAsync(count);
        var response = active
            .Select(topic =>
            {
                var member = members.One(topic.memberId);
                return new TopicFetchResponse(topic,member);
            })
            .ToList();
        return new(response);
    }

    [HttpPost]
    public async Task<JsonResult> AddTopic(TopicUpdateRequest request)
    {
        var member = members.One(User);
        if (member is null) return new("") { StatusCode = StatusCodes.Status404NotFound };
        var topicId = await topics.AddAsync(request, member);
        return new(topicId);
    }

    [HttpPatch]
    public async Task<JsonResult> UpdateTopic(TopicUpdateRequest request)
    {
        var member = members.One(User);
        if (member is null) return new("") { StatusCode = StatusCodes.Status404NotFound };
        var result = await topics.UpdateAsync(request, member);
        return new(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<JsonResult> RemoveTopic(int id)
    {
        var member = members.One(User);
        if (member is null) return new("") { StatusCode = StatusCodes.Status404NotFound };
        var result = await topics.RemoveAsync(id,member);
        return new(result);
    }
}