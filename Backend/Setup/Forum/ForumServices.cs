using ALBackend.Persistence.Forum;
using ALBackend.Services.Forum;

namespace ALBackend.Setup.Forum;

public static class ForumServices
{
    public static void Inject(WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ForumDb>();
        builder.Services.AddScoped<IForumFetcher, ForumFetcher>();
        builder.Services.AddScoped<IForumUpdater, ForumUpdater>();
    }
}