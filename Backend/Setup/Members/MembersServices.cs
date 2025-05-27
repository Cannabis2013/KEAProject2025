using ALBackend.Persistence.Members;
using ALBackend.Services.Members;

namespace ALBackend.Setup.Members;

public static class MembersServices
{
    public static void Inject(WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<MembersDb>();
        builder.Services.AddScoped<IMembersFetcher, MembersFetcher>();
        builder.Services.AddScoped<IMembersUpdate, MembersUpdate>();
    }
}