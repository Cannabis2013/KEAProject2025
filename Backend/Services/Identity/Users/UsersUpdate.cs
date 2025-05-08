using ALBackend.Entities.Identity;
using ALBackend.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ALBackend.Services.Identity.Users;

public class UsersUpdate(UserManager<UserAccount> userManager, IdentityDb users) : IUsersUpdate
{
    private async Task<bool> UsernameExists(string username)
    {
        var user = await userManager.FindByNameAsync(username);
        return user != null;
    }

    private async Task _create(UserCredentials credentials)
    {
        var user = credentials.ToUser();
        var result = await userManager.CreateAsync(user, credentials.Password);
        if (!result.Succeeded)
            throw new("Failed to create user");
        foreach (var role in credentials.Roles)
            await userManager.AddToRoleAsync(user, role);
    }

    public async Task<Guid> CreateAsync(UserCredentials credentials)
    {
        if (await UsernameExists(credentials.UserName))
            throw new("Duplicate mail");
        await _create(credentials);
        var newUser = await userManager.FindByNameAsync(credentials.UserName);
        if (newUser is null)
            throw new("Something went wrong");
        return Guid.Parse(newUser.Id);
    }

    public async Task RemoveAsync(Guid id)
    {
        var user = await userManager.FindByIdAsync(id.ToString());
        if (user is null) return;
        await userManager.DeleteAsync(user);
    }

    public async Task UpdateAsync(UserCredentials request)
    {
        var user = await users
            .Users
            .FirstOrDefaultAsync(user => user.UserName == request.UserName);
        if (user is null) return;
        user.Email = request.Email;
        user.PhoneNumber = request.PhoneNumber;
        user.PasswordHash = userManager.PasswordHasher.HashPassword(user, request.Password);
        var userRoles = await userManager.GetRolesAsync(user);
        await userManager.RemoveFromRolesAsync(user, userRoles);
        await userManager.AddToRolesAsync(user, request.Roles);
        await userManager.UpdateAsync(user);
    }
}