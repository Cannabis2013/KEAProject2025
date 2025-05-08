using ALBackend.DataTransferObject.Identity;
using ALBackend.Entities.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ALBackend.Services.Identity.Authentication;

public interface IAuthentication
{
    public Task<JsonResult> SignIn(LoginCredentials credentials);
    public Task<JsonResult> SignInAsAdmin(LoginCredentials credentials);
    public Task<JsonResult> Refresh(JwtCredentials credentials);
}