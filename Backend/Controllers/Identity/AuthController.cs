using ALBackend.DataTransferObject.Identity;
using ALBackend.Entities.Identity;
using ALBackend.Services.Identity.Authentication;
using ALBackend.Services.Identity.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ALBackend.Controllers.Identity;

[ApiController, Route("/auth")]
public class AuthController(
    IAuthentication authorization,
    IUsersFetch usersFetch,
    UserManager<UserAccount> userManager)
    : ControllerBase
{
    [HttpGet, Route("check"), Authorize]
    public bool Check() => true;

    [HttpGet, Route("/auth/{id:guid}"), Authorize]
    public JsonResult Get(Guid id)
    {
        var user = usersFetch.User(id);
        if (user is null) return new(""){StatusCode = 404};
        var roles = userManager.GetRolesAsync(user!).Result;
        return new(user.ToFullDto(roles));
    }

    [HttpPost, Route("login")]
    public async Task<JsonResult> Login([FromBody] LoginCredentials credentials) =>
        await authorization.SignIn(credentials);

    [HttpPost, Route("refresh")]
    public async Task<JsonResult> Refresh([FromBody] JwtCredentials credentials) =>
        await authorization.Refresh(credentials);
}