using ALMembers.Entities;
using Microsoft.EntityFrameworkCore;

namespace ALBackend.Persistence.Members;

public class MembersDb(IConfiguration configuration) : DbContext
{
    public DbSet<Member> Members { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = ConnectionString();
        optionsBuilder.UseMySql(connectionString, new MariaDbServerVersion(new Version()));
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Member>()
            .Property(m => m.CreatedAt)
            .HasDefaultValueSql("now()");
    }

    private string ConnectionString()
    {
        var userName = configuration.GetValue<string>("DbUser");
        var pass = configuration.GetValue<string>("DbPass");
        var host = configuration.GetValue<string>("DbHost");
        return $"server={host};user id={userName};password={pass};database=ALResources;";
    }
}