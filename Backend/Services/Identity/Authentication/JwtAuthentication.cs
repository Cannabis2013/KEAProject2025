using ALBackend.DataTransferObject.Identity;
using ALBackend.Entities.Identity;
using ALBackend.Services.Identity.Token;
using ALBackend.Services.Identity.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ALBackend.Services.Identity.Authentication;

public class JwtAuthentication(
    IUsersFetcher usersFetcher,
    SignInManager<UserAccount> signInManager,
    UserManager<UserAccount> userManager,
    ISecurityToken tokenBuilder)
    : IAuthentication
{
    public async Task<JsonResult> SignIn(LoginRequest request)
    {
        if (!await AttemptSignIn(request))
            return new("") { StatusCode = StatusCodes.Status403Forbidden };
        var user = usersFetcher.User(request);
        if (user == null)
            return new(request) { StatusCode = StatusCodes.Status401Unauthorized };
        await UpdateRefreshToken(user);
        var token = await tokenBuilder.Create(user);
        return new(new
        {
            Email = user.UserName,
            AccessToken = token,
            user.RefreshToken,
            user.Id
        });
    }

    public async Task<JsonResult> SignInAsAdmin(LoginRequest request)
    {
        if (!await AttemptSignIn(request))
            return new("") { StatusCode = StatusCodes.Status403Forbidden };
        var user = usersFetcher.User(request);
        if (user == null)
            return new(request) { StatusCode = StatusCodes.Status401Unauthorized };
        var roles = await userManager.GetRolesAsync(user);
        if(!roles.Contains("CHAIRMAN") && !roles.Contains("DEPUTY_CHAIRMAN"))
            return new("") { StatusCode = StatusCodes.Status403Forbidden };
        await UpdateRefreshToken(user);
        var token = await tokenBuilder.Create(user);
        return new(new
        {
            Email = user.UserName,
            AccessToken = token,
            user.RefreshToken,
            user.Id
        });
    }

    public async Task<JsonResult> Refresh(JwtCredentials credentials)
    {
        var user = usersFetcher.User(credentials);
        if (user == null)
            return new("") { StatusCode = StatusCodes.Status403Forbidden };
        await signInManager.RefreshSignInAsync(user);
        await UpdateRefreshToken(user);
        var token = await tokenBuilder.Create(user);
        return new(new
        {
            user.UserName,
            AccessToken = token,
            user.RefreshToken
        });
    }

    private async Task UpdateRefreshToken(UserAccount user)
    {
        user.RefreshToken = Guid.NewGuid().ToString();
        await userManager.UpdateAsync(user);
    }

    private async Task<bool> AttemptSignIn(LoginRequest request)
    {
        var signIn = await signInManager.PasswordSignInAsync(request.Email, request.Password, true, false);
        return signIn.Succeeded;
    }
}