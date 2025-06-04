using ALBackend.DataTransferObject.Identity;
using ALBackend.Entities.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ALBackend.Services.Identity.Authentication;

public interface IAuthentication
{
    public Task<JsonResult> SignIn(LoginRequest request);
    public Task<JsonResult> SignInAsAdmin(LoginRequest request);
    public Task<JsonResult> Refresh(JwtCredentials credentials);
}