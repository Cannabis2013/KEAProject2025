using ALBackend.Entities.Members;
using ALBackend.Persistence.Shared;
using ALMembers.Entities;
using Microsoft.EntityFrameworkCore;

namespace ALBackend.Persistence.Members;

public class MembersDb(IConfiguration configuration) : MariaDbContext(configuration)
{
    public DbSet<Member> Members { get; set; }
    public DbSet<ImageLink> ImageLinks { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Member>()
            .Property(m => m.CreatedAt)
            .HasDefaultValueSql("now()");
    }
}