using System.Net;
using ALBackend.DataTransferObject.Members;
using ALBackend.Services.Identity.Users;
using ALBackend.Services.Members;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ALBackend.Controllers.Members;

[ApiController, Route("/members")]
public class MembersController(
    IMembersFetcher membersFetcher,
    IMembersUpdate membersUpdate,
    IUsersFetcher usersFetcher) : Controller
{
    [HttpGet, Authorize(Roles = "CHAIRMAN, DEPUTY_CHAIRMAN")]
    public JsonResult GetMembers()
    {
        var members = membersFetcher.Many()
            .Select(member =>
            {
                var user = usersFetcher.User(member.UserId);
                member.Email = user?.Email;
                member.UserId = member.UserId;
                return member;
            })
            .ToList();
        return new(members);
    }

    // GET
    [HttpGet("/members/{memberId:int}"), Authorize]
    public async Task<JsonResult> GetMember(int memberId)
    {
        var member = membersFetcher.One(memberId);
        if (member is null) return new("Member not found") { StatusCode = StatusCodes.Status404NotFound };
        var user = await usersFetcher.UserWithRoles(member.UserId);
        if(user is null) return new("User not found") { StatusCode = StatusCodes.Status404NotFound };
        member.Roles = user?.Roles;
        return new(member);
    }

    [HttpGet("/members/user/{userId:guid}"), Authorize]
    public async Task<JsonResult> GetMember(Guid userId)
    {
        var user = await usersFetcher.UserWithRoles(userId);
        var response = membersFetcher.One(userId);
        if (response is null) return new("") { StatusCode = StatusCodes.Status404NotFound };
        response.Roles = user?.Roles;
        return new(response);
    }

    [HttpPost, Authorize(Roles = "CHAIRMAN,DEPUTY_CHAIRMAN")]
    public async Task<JsonResult> CreateMember(MemberInfo request)
    {
        var result = await membersUpdate.Create(request);
        return new(result) { StatusCode = StatusCodes.Status201Created };
    }

    [HttpPatch, Authorize(Roles = "CHAIRMAN,DEPUTY_CHAIRMAN")]
    public async Task<JsonResult> UpdateMember(MemberInfo request)
    {
        var result = await membersUpdate.Update(request);
        return new(result) { StatusCode = StatusCodes.Status200OK };
    }


    [HttpDelete("/members/{memberId:int}"), Authorize(Roles = "CHAIRMAN,DEPUTY_CHAIRMAN,IT")]
    public async Task<JsonResult> DeleteMember(int memberId)
    {
        var result = await membersUpdate.RemoveAsync(memberId);
        return new(result) { StatusCode = StatusCodes.Status200OK };
    }
}