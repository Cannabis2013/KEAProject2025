using ALBackend.DataTransferObject.Members;
using ALBackend.Services.Identity.Users;
using ALBackend.Services.Members;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ALBackend.Controllers.Members;

[ApiController, Route("/members")]
public class MembersController(
    IMembers members,
    IUsersFetcher usersFetcher,
    IProfileImages profileImages) : Controller
{
    [HttpGet, Authorize(Roles = "CHAIRMAN, DEPUTY_CHAIRMAN")]
    public async Task<JsonResult> GetMembers()
    {
        var response = (await members.ManyAsync())
            .Select(member =>
            {
                var user = usersFetcher.User(member.UserId);
                return new MemberFetchResponse(member,user);
            })
            .ToList();
        return new(response);
    }

    // GET
    [HttpGet("/members/{memberId:int}"), Authorize]
    public async Task<JsonResult> GetMember(int memberId)
    {
        var member = members.One(memberId);
        if (member is null) return new("Member not found") { StatusCode = StatusCodes.Status404NotFound };
        
        var userWithRoles = await usersFetcher.UserWithRoles(member.UserId);
        if(userWithRoles is null) return new("User not found") { StatusCode = StatusCodes.Status404NotFound };
        var response = new MemberFetchResponse(member,userWithRoles.User)
        {
            Roles = userWithRoles.Roles,
            ProfileImageAsBase64 = profileImages.One(member.ProfileImageId)?.Blob
        };
        return new(response);
    }

    [HttpGet("/members/user/{userId:guid}"), Authorize]
    public async Task<JsonResult> GetMember(Guid userId)
    {
        var user = await usersFetcher.UserWithRoles(userId);
        if(user is null) return new("User not found") { StatusCode = StatusCodes.Status404NotFound };
        
        var member = members.One(userId);
        if (member is null) return new("") { StatusCode = StatusCodes.Status404NotFound };
        
        var response = new MemberFetchResponse(member,user.User)
        {
            Roles = user.Roles,
            ProfileImageAsBase64 = profileImages.One(member.ProfileImageId)?.Blob
        };
        return new(response);
    }

    [HttpPost, Authorize(Roles = "CHAIRMAN,DEPUTY_CHAIRMAN")]
    public async Task<JsonResult> CreateMember(MemberUpdateRequest request)
    {
        var result = await members.Create(request);
        return new(result) { StatusCode = StatusCodes.Status201Created };
    }

    [HttpPatch, Authorize(Roles = "CHAIRMAN,DEPUTY_CHAIRMAN")]
    public async Task<JsonResult> UpdateMember(MemberUpdateRequest request)
    {
        var result = await members.Update(request);
        return new(result) { StatusCode = StatusCodes.Status200OK };
    }


    [HttpDelete("/members/{memberId:int}"), Authorize(Roles = "CHAIRMAN,DEPUTY_CHAIRMAN,IT")]
    public async Task<JsonResult> DeleteMember(int memberId)
    {
        var result = await members.RemoveAsync(memberId);
        return new(result) { StatusCode = StatusCodes.Status200OK };
    }
}