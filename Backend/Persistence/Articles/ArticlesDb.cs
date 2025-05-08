using ALBackend.Entities.Articles;
using Microsoft.EntityFrameworkCore;

namespace ALBackend.Persistence.Articles;

public class ArticlesDb(IConfiguration configuration) : DbContext
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
        return $"server={host};user id={userName};password={pass};database=ALResources;";
    }
}