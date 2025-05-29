using ALBackend.Services.Articles;
using ALBackend.Services.Forum;
using ALBackend.Services.Members;

namespace ALBackend.Setup;

public static class ServicesInjector
{
    public static void Inject(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IMembersFetcher, MembersFetcher>();
        builder.Services.AddScoped<IMembersUpdate, MembersUpdate>();
        
        builder.Services.AddScoped<IArticlesFetcher, ArticlesFetcher>();
        builder.Services.AddScoped<IArticlesUpdater, ArticlesUpdater>();
        
        builder.Services.AddScoped<ITopicFetcher, TopicFetcher>();
        builder.Services.AddScoped<ITopicUpdater, TopicUpdater>();
        builder.Services.AddScoped<IPostFetcher, PostFetcher>();
        builder.Services.AddScoped<IPostUpdater, PostUpdater>();
    }
}