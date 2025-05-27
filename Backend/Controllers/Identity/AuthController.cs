using ALBackend.DataTransferObject.Identity;
using ALBackend.Entities.Identity;
using ALBackend.Services.Identity.Authentication;
using ALBackend.Services.Identity.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ALBackend.Controllers.Identity;

[ApiController, Route("/auth")]
public class AuthController(
    IAuthentication authorization,
    IUsersFetcher usersFetcher)
    : ControllerBase
{
    [HttpGet, Route("check"), Authorize]
    public bool Check() => true;

    [HttpGet, Route("/auth/{id:guid}"), Authorize]
    public async Task<JsonResult> Get(Guid id)
    {
        var user = await usersFetcher.UserWithRoles(id);
        return user is not null ? new(user) : new(""){StatusCode = 404};
    }

    [HttpPost, Route("login")]
    public async Task<JsonResult> Login([FromBody] LoginCredentials credentials) =>
        await authorization.SignIn(credentials);

    [HttpPost, Route("refresh")]
    public async Task<JsonResult> Refresh([FromBody] JwtCredentials credentials) =>
        await authorization.Refresh(credentials);
}