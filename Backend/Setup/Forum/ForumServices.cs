using ALBackend.Persistence.Forum;

namespace ALBackend.Setup.Forum;

public static class ForumServices
{
    public static void Inject(WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ForumDb>();
    }
}