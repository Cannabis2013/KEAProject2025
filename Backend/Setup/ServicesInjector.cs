using ALBackend.Services.Articles;
using ALBackend.Services.Forum;
using ALBackend.Services.Members;

namespace ALBackend.Setup;

public static class ServicesInjector
{
    public static void Inject(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IMembers, Members>();
        builder.Services.AddScoped<IProfileImages, ProfileImages>();
        
        builder.Services.AddScoped<IArticles, Articles>();
        
        builder.Services.AddScoped<ITopicFetcher, TopicFetcher>();
        builder.Services.AddScoped<ITopicUpdater, TopicUpdater>();
        builder.Services.AddScoped<IPostFetcher, PostFetcher>();
        builder.Services.AddScoped<IPostUpdater, PostUpdater>();
    }
}