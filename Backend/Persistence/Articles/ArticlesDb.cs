using ALBackend.Entities.Articles;
using ALBackend.Persistence.Shared;
using Microsoft.EntityFrameworkCore;

namespace ALBackend.Persistence.Articles;

public class ArticlesDb(IConfiguration configuration) : MariaDbContext(configuration)
{
    public DbSet<Article> Articles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>()
            .Property(article => article.CreatedAt)
            .HasDefaultValueSql("now()");
        modelBuilder.Entity<Article>()
            .Property(article => article.UpdatedAt)
            .HasDefaultValueSql("now()");
    }
}