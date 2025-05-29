using ALBackend.Entities.Articles;
using ALBackend.Entities.Events;
using ALBackend.Entities.Forum;
using ALBackend.Entities.Members;
using ALMembers.Entities;
using Microsoft.EntityFrameworkCore;

namespace ALBackend.Persistence;

public class MariaDbContext(IConfiguration configuration) : DbContext
{
    public DbSet<Member> Members { get; set; }
    public DbSet<ImageLink> ImageLinks { get; set; }
    public DbSet<Article> Articles { get; set; }
    public DbSet<Topic> Topics { get; set; }
    public DbSet<Post> Posts { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Member>()
            .Property(t => t.CreatedAt)
            .HasDefaultValueSql("now()");
        
        modelBuilder.Entity<Member>()
            .Property(t => t.UpdatedAt)
            .HasDefaultValueSql("now()");
        
        modelBuilder.Entity<Article>()
            .Property(t => t.CreatedAt)
            .HasDefaultValueSql("now()");
        
        modelBuilder.Entity<Article>()
            .Property(t => t.UpdatedAt)
            .HasDefaultValueSql("now()");
        
        modelBuilder.Entity<Topic>()
            .Property(t => t.CreatedAt)
            .HasDefaultValueSql("now()");
        
        modelBuilder.Entity<Topic>()
            .Property(t => t.UpdatedAt)
            .HasDefaultValueSql("now()");
        
        modelBuilder.Entity<Post>()
            .Property(t => t.CreatedAt)
            .HasDefaultValueSql("now()");
        
        modelBuilder.Entity<Post>()
            .Property(t => t.UpdatedAt)
            .HasDefaultValueSql("now()");
        
        modelBuilder.Entity<Event>()
            .Property(t => t.CreatedAt)
            .HasDefaultValueSql("now()");
        
        modelBuilder.Entity<Event>()
            .Property(t => t.UpdatedAt)
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