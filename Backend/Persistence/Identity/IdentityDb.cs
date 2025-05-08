using ALBackend.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ALBackend.Persistence.Identity;

public class IdentityDb(IConfiguration configuration) : IdentityDbContext<UserAccount>
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = ConnectionString();
        optionsBuilder.UseMySql(connectionString,new MariaDbServerVersion(new Version()));
        base.OnConfiguring(optionsBuilder);
    }
    private string ConnectionString()
    {
        var userName = configuration.GetValue<string>("DbUser");
        var pass = configuration.GetValue<string>("DbPass");
        var host = configuration.GetValue<string>("DbHost");
        return $"server={host};user id={userName};password={pass};database=AccountsDb;";
    }
}