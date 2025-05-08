using ALBackend.Persistence.Articles;
using ALBackend.Services.Articles;

namespace ALBackend.Setup.Articles;

public static class NewsServices
{
    public static void Inject(WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ArticlesDb>();
        builder.Services.AddScoped<IArticlesFetcher, ArticlesFetcher>();
        builder.Services.AddScoped<IArticlesUpdater, ArticlesUpdater>();
    }
}