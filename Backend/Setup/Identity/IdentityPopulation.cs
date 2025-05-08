using ALBackend.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace ALBackend.Setup.Identity;

public class IdentityPopulation
{
    private IdentityPopulation(WebApplication app)
    {
        _app = app;
    }

    public static IdentityPopulation Instance(IHost app) =>
        new(app as WebApplication ?? throw new InvalidOperationException());

    public async Task PopulateAsync()
    {
        try
        {
            using var scope = _app.Services.CreateScope();
            InitServices(scope.ServiceProvider);
            var roles = await CreateRoles();
            await CreateUsers(roles);
        }
        catch
        {
            // ignored
        }
    }

    private void InitServices(IServiceProvider provider)
    {
        _appConfig = provider.GetRequiredService<IConfiguration>();
        _userManager = provider.GetRequiredService<UserManager<UserAccount>>();
        _roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
    }

    private async Task<List<IdentityRole>> CreateRoles()
    {
        List<string> roles = ["CHAIRMAN", "DEPUTY_CHAIRMAN", "CASHIER", "IT","MEMBER", "PROSPECT", "HANG_AROUND"];
        var roleObjects = roles.Select(role => new IdentityRole(role)).ToList();
        foreach (var role in roleObjects)
        {
            if (!await _roleManager!.RoleExistsAsync(role.Name ?? ""))
                await _roleManager.CreateAsync(role);
        }

        return roleObjects;
    }

    private async Task CreateUsers(IList<IdentityRole> roles)
    {
        await CreateUser(roles, ["CHAIRMAN","MEMBER"], "hjalte@gmail.com");
        await CreateUser(roles, ["DEPUTY_CHAIRMAN","MEMBER"], "lars@gmail.com");
        await CreateUser(roles, ["CASHIER","MEMBER"], "clemme@gmail.com");
    }

    private async Task CreateUser(IList<IdentityRole> availableRoles, IList<string> userRoles, string email)
    {
        var roles = availableRoles
            .Where(r => userRoles.Contains(r.Name!));
        var user = new UserAccount()
        {
            UserName = email,
            Email = email
        };
        var result = _userManager!.FindByEmailAsync(email).Result;
        if (result is not null) 
            return;
        var pass = _appConfig?.GetValue<string>("APass");
        var createUser = await _userManager.CreateAsync(user, pass!);
        if (!createUser.Succeeded)
            return;
        foreach (var role in roles)
            await _userManager.AddToRoleAsync(user, role.Name!);
    }

    private readonly WebApplication _app;
    private UserManager<UserAccount>? _userManager;
    private RoleManager<IdentityRole>? _roleManager;
    private IConfiguration? _appConfig;
}