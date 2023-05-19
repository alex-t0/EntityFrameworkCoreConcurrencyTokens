using Microsoft.EntityFrameworkCore;

namespace WebApp.Db;

public class AwesomeDbContext : DbContext
{
    public AwesomeDbContext(DbContextOptions<AwesomeDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<DbAwesomeEntity> AwesomeEntities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        if (Database.IsSqlServer())
        {
            modelBuilder.Entity<DbAwesomeEntity>().Ignore(b => b.Xmin);
        }
        else if (Database.IsNpgsql())
        {
            modelBuilder.Entity<DbAwesomeEntity>().Ignore(b => b.Timestamp);
        }
    }
}