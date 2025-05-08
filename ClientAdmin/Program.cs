using ALAdmin;
using ALAdmin.DataTransferObjects.Members;
using ALAdmin.Models.Members;
using ALAdmin.Services.Auth.Authentication;
using ALAdmin.Services.Auth.Manager;
using ALAdmin.Services.Auth.Request;
using ALAdmin.Services.Auth.Storage;
using ALAdmin.Services.Members;
using Blazored.LocalStorage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddScoped<IAuthStorage, AuthStorage>()
    .AddBlazoredLocalStorage()
    .AddScoped<IAuthRequest, JwtBearerRequest>()
    .AddScoped<IAuthManager, AuthManager>()
    .AddScoped<IAuthentication, JwtAuthentication>()
    .AddScoped<IFilter<FilterValues, MemberListItem>, MembersFilter>()
    .AddScoped(sp => new HttpClient()
    {
        BaseAddress = new("https://localhost:7226")
    })
    .AddRazorComponents()
    .AddInteractiveServerComponents();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.Run();