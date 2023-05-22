using Microsoft.EntityFrameworkCore;
using WebApp.Models;

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
        
        /*if (Database.IsSqlServer())
        {
            modelBuilder.Entity<DbAwesomeEntity>().Ignore(b => b.Xmin);
        }
        else if (Database.IsNpgsql())
        {
            modelBuilder.Entity<DbAwesomeEntity>().Ignore(b => b.Timestamp);
        }*/

        var entityBuilder = modelBuilder.Entity<DbAwesomeEntity>();
        
        entityBuilder.OwnsOne(x => x.Timestamp, builder =>
        {
            if (Database.IsSqlServer())
            {
                builder.Property(z => z.Timestamp)
                    .HasColumnName(nameof(ComplexTimestamp.Timestamp).ToLowerInvariant())
                    /*.IsRowVersion()*/;
                builder.Ignore(z => z.Xmin);
            }
            else if (Database.IsNpgsql())
            {
                builder.Property(z => z.Xmin)
                    .HasColumnName(nameof(ComplexTimestamp.Xmin).ToLowerInvariant());
                    // .HasColumnType("xid")
                    // .ValueGeneratedOnAddOrUpdate()
                    // .IsConcurrencyToken();
                builder.Ignore(z => z.Timestamp);
                /*builder.Ignore(z => z.Xmin);*/
            }
            else
            {
                throw new InvalidOperationException("Only MS SQL and Pgsql are supported");
            }
        });
    }
}