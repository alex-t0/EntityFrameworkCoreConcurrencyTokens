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
            // modelBuilder.Entity<DbAwesomeEntity>().Ignore(b => b.Xmin);
            modelBuilder.Entity<DbAwesomeEntity>().Property(x => x.Timestamp).HasColumnName("Timestamp").HasColumnType("Timestamp");
        }
        else if (Database.IsNpgsql())
        {
            // modelBuilder.Entity<DbAwesomeEntity>().Ignore(b => b.Timestamp);
            modelBuilder.Entity<DbAwesomeEntity>().Property(x => x.Timestamp)
                .HasColumnName("xmin").HasColumnType("xid")
                .HasConversion(to => (uint)to, from => (object) from);
        }
    }
}