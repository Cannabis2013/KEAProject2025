using ALBackend.DataTransferObject.Members;
using ALBackend.Services.Members;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ALBackend.Controllers.Members;

[ApiController, Route("/members")]
public class MembersController(
    IMembersFetch membersFetch,
    IMembersUpdate membersUpdate) : Controller
{
    [HttpGet, Authorize(Roles = "CHAIRMAN, DEPUTY_CHAIRMAN")]
    public JsonResult GetMembers() => membersFetch.All();

    // GET
    [HttpGet("/members/{memberId:int}"), Authorize]
    public async Task<JsonResult> GetMember(int memberId) => await membersFetch.One(memberId);
    
    [HttpGet("/members/user/{userId:guid}"), Authorize]
    public async Task<JsonResult> GetMembers(Guid userId) => await membersFetch.One(userId);

    [HttpPost, Authorize(Roles = "CHAIRMAN,DEPUTY_CHAIRMAN")]
    public async Task<JsonResult> CreateMember(MemberInfo request)
    {
        try
        {
            await membersUpdate.Create(request);
        }
        catch (Exception e)
        {
            return new(e.Message) { StatusCode = StatusCodes.Status500InternalServerError };
        }

        return new("") { StatusCode = StatusCodes.Status201Created };
    }

    [HttpPatch, Authorize(Roles = "CHAIRMAN,DEPUTY_CHAIRMAN")]
    public async Task<JsonResult> UpdateMember(MemberInfo request)
    {
        try
        {
            await membersUpdate.Update(request);
        }
        catch (Exception)
        {
            return new("Member not updated correctly. Please consult your administrator!") { StatusCode = StatusCodes.Status500InternalServerError };
        }
        return new("") { StatusCode = StatusCodes.Status200OK };
    }


    [HttpDelete("/members/{memberId:int}"),Authorize(Roles = "CHAIRMAN,DEPUTY_CHAIRMAN,IT")]
    public async Task<JsonResult> DeleteMember(int memberId)
    {
        try
        {
            await membersUpdate.RemoveAsync(memberId);
        }
        catch (Exception e)
        {
            return new(e.Message){StatusCode = StatusCodes.Status500InternalServerError};
        }
        return new("") { StatusCode = StatusCodes.Status200OK };
    }
}