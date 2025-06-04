using ALBackend.Services.Articles;
using ALBackend.Services.Forum;
using ALBackend.Services.ImageGallary;
using ALBackend.Services.Members;

namespace ALBackend.Setup;

public static class ServicesInjector
{
    public static void Inject(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IMembers, Members>();
        builder.Services.AddScoped<IProfileImages, ProfileImages>();

        builder.Services.AddScoped<Images, Images>();
        
        builder.Services.AddScoped<IArticles, Articles>();
        
        builder.Services.AddScoped<ITopics, Topics>();
        builder.Services.AddScoped<ITopicResponse, TopicResponse>();
        builder.Services.AddScoped<IPosts, Posts>();
    }
}