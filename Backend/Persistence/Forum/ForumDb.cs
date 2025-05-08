using ALBackend.Entities.Forum;
using Microsoft.EntityFrameworkCore;

namespace ALBackend.Persistence.Forum;

public class ForumDb(IConfiguration configuration) : DbContext
{
    public DbSet<Topic> Topics { get; set; }
    public DbSet<Post> Posts { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Topic>()
            .Property(t => t.CreatedAt)
            .HasDefaultValueSql("now()");
        modelBuilder.Entity<Topic>()
            .Property(t => t.UpdatedAt)
            .HasDefaultValueSql("now()");
        modelBuilder.Entity<Post>()
            .Property(m => m.CreatedAt)
            .HasDefaultValueSql("now()");
        modelBuilder.Entity<Post>()
            .Property(m => m.UpdatedAt)
            .HasDefaultValueSql("now()");
    }
    
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