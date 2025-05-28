using Microsoft.EntityFrameworkCore;

namespace ALBackend.Persistence.Shared;

public class MariaDbContext(IConfiguration configuration) : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = ConnectionString();
        optionsBuilder.UseMySql(connectionString, new MariaDbServerVersion(new Version()));
        base.OnConfiguring(optionsBuilder);
    }
    
    private string ConnectionString()
    {
        var userName = configuration.GetValue<string>("DbUser");
        var pass = configuration.GetValue<string>("DbPass");
        var host = configuration.GetValue<string>("DbHost");
        return $"server={host};user id={userName};password={pass};database=ALResources;";
    }
    
}