using ALBackend.Entities.Identity;
using ALBackend.Persistence.Identity;
using ALBackend.Services.Identity.Authentication;
using ALBackend.Services.Identity.Token;
using ALBackend.Services.Identity.Users;
using Microsoft.AspNetCore.Identity;

namespace ALBackend.Setup.Identity;

public static class SecurityServices
{
    public static void Inject(WebApplicationBuilder builder)
    {
        InjectIdentityServices(builder);
        InjectJwtServices(builder);
    }

    private static void InjectJwtServices(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAuthentication, Services.Identity.Authentication.JwtAuthentication>();
        builder.Services.AddScoped<IUsersFetcher,UsersFetcher>();
        builder.Services.AddScoped<IUsersUpdate,UsersUpdate>();
        builder.Services.AddScoped<ISecurityToken,JwtAuthorizationToken>();
    }

    private static void InjectIdentityServices(WebApplicationBuilder builder)
    {
        builder.Services
            .AddIdentity<UserAccount,IdentityRole>(o => 
                o.SignIn.RequireConfirmedAccount = false)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<IdentityDb>()
            .AddDefaultTokenProviders();
        builder.Services.AddDbContext<IdentityDb>();
        builder.Services.Configure<IdentityOptions>(Config);
    }
    
    private static void Config(IdentityOptions options)
    {
        // Password settings.
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 8;
        options.Password.RequiredUniqueChars = 0;
                
        // User settings.
        options.User.AllowedUserNameCharacters =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        options.User.RequireUniqueEmail = false;
    }
}