using ALBackend.Entities.Articles;
using ALBackend.Entities.Events;
using ALBackend.Entities.Forum;
using ALBackend.Entities.ImageGallary;
using ALBackend.Entities.Members;
using ALBackend.Entities.Shared;
using ALMembers.Entities;
using Microsoft.EntityFrameworkCore;

namespace ALBackend.Persistence;

public class MariaDbContext(IConfiguration configuration) : DbContext
{
    public DbSet<Member> Members { get; set; }
    public DbSet<ProfileImage> ProfileImages { get; set; }
    public DbSet<Article> Articles { get; set; }
    public DbSet<Topic> Topics { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Image> Images { get; set; }

    private static void ConfigureTimestamps<TEntity>(ModelBuilder modelBuilder) where TEntity : Entity
    {
        modelBuilder.Entity<TEntity>()
            .Property(t => t.CreatedAt)
            .HasDefaultValueSql("now()");
        
        modelBuilder.Entity<TEntity>()
            .Property(t => t.UpdatedAt)
            .HasDefaultValueSql("now()");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureTimestamps<Member>(modelBuilder);
        ConfigureTimestamps<Article>(modelBuilder);
        ConfigureTimestamps<Topic>(modelBuilder);
        ConfigureTimestamps<Post>(modelBuilder);
        ConfigureTimestamps<Event>(modelBuilder);
        ConfigureTimestamps<Image>(modelBuilder);
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