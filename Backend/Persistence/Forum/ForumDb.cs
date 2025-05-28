using ALBackend.Entities.Forum;
using ALBackend.Persistence.Shared;
using Microsoft.EntityFrameworkCore;

namespace ALBackend.Persistence.Forum;

public class ForumDb(IConfiguration configuration) : MariaDbContext(configuration)
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
}