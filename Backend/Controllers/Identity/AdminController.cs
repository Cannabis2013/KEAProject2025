using ALBackend.DataTransferObject.Identity;
using ALBackend.Services.Identity.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ALBackend.Controllers.Identity;

[ApiController, Route("/auth/admin")]
public class AdminController(
    IAuthentication authentication) : ControllerBase
{
    [HttpGet, Route("check"), Authorize(Roles = "CHAIRMAN,DEPUTY_CHAIRMAN")]
    public bool Check() => true;
    
    [HttpPost, Route("login")]
    public async Task<JsonResult> Login([FromBody] LoginRequest request) =>
        await authentication.SignInAsAdmin(request);
}